﻿using Project.BL.DTOs.OrderDTOs;
using Project.BL.Models;
using Project.BL.Services.InternalServices.Abstractions;
using Project.DAL.Repositories.Abstractions.Campaign;
using Project.DAL.Repositories.Abstractions.Order;
using Project.DAL.Repositories.Abstractions.Product;
using Project.DAL.Repositories.Abstractions.Worker;
using Microsoft.AspNetCore.Http;
using Project.Core.Entities;
using System.Security.Claims;
using Project.Core.Entities.Commons;
using Project.BL.DTOs.ProductDTOs;
namespace Project.BL.Services.InternalServices.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IWorkerReadRepository _workerReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICampaignReadRepository _campaignReadRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IHttpContextAccessor httpContextAccessor, IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICampaignReadRepository campaignReadRepository, IProductReadRepository productReadRepository, IWorkerReadRepository workerReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _campaignReadRepository = campaignReadRepository;
            _productReadRepository = productReadRepository;
            _workerReadRepository = workerReadRepository;
        }

        public async Task<ApiResponse<OrderCreateResponseDTO>> CreateAsync(OrderCreateDTO dto)
        {
            try
            {
                var workerIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (workerIdClaim == null)
                {
                    return new ApiResponse<OrderCreateResponseDTO> { IsSuccess = false, Message = "Unauthorized" };
                }

                var workerId = int.Parse(workerIdClaim.Value);
                var worker = await _workerReadRepository.GetByIdAsync(workerId, true, "District");
                if (worker == null)
                {
                    return new ApiResponse<OrderCreateResponseDTO> { IsSuccess = false, Message = "Worker not found" };
                }

                var product = await _productReadRepository.GetByIdAsync(dto.ProductId, true, "ProductDistrictPrices");
                if (product == null)
                {
                    return new ApiResponse<OrderCreateResponseDTO> { IsSuccess = false, Message = "Product not found" };
                }

                decimal finalPrice = product.Price;

                var campaigns = await _campaignReadRepository.GetAllAsync(false);
                var activeCampaign = campaigns.FirstOrDefault(c =>
                    c.IsActive &&
                    DateTime.UtcNow >= c.StartDate &&
                    DateTime.UtcNow <= c.EndDate
                    );

                if (activeCampaign != null)
                {
                    finalPrice *= (1 - activeCampaign.DiscountPercent / 100m);
                }

                var districtPriceEntity = product.ProductDistrictPrices
                    .FirstOrDefault(p => p.DistrictId == worker.DistrictId);

                if (districtPriceEntity != null)
                {
                    finalPrice -= districtPriceEntity.Price;
                }

                decimal totalPrice = finalPrice * dto.ProductCount;

                var order = new Order
                {
                    ProductId = dto.ProductId,
                    ProductCount = dto.ProductCount,
                    WorkerId = workerId,
                    TotalPrice = totalPrice
                };
                order.CreatedAt = DateTime.UtcNow.AddHours(4);
                await _orderWriteRepository.CreateAsync(order);
                await _orderWriteRepository.SaveChangeAsync();

                return new ApiResponse<OrderCreateResponseDTO>
                {
                    IsSuccess = true,
                    Data = new OrderCreateResponseDTO
                    {
                        TotalPrice = totalPrice
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<OrderCreateResponseDTO>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<PagedResult<OrderReadDTO>> GetPaginatedAsync(PaginationParams @params)
        {
            var orders = await _orderReadRepository.GetAllAsync(false, "Product");
            var campaigns = await _campaignReadRepository.GetAllAsync(false);

            var orderDTOs = orders.Select(o =>
            {
                var activeCampaign = campaigns.FirstOrDefault(c =>
                    c.IsActive &&
                    c.StartDate <= o.CreatedAt &&
                    c.EndDate >= o.CreatedAt);

                return new OrderReadDTO
                {
                    Id = o.Id,
                    ProductId = o.ProductId,
                    ProductCount = o.ProductCount,
                    ProductTitle = o.Product.Title,
                    ProductPrice = o.Product.Price,
                    CampaignId = activeCampaign?.Id ?? 0,
                    CampaignName = activeCampaign?.Name ?? string.Empty,
                    TotalPrice = o.TotalPrice,
                    CreatedAt = o.CreatedAt
                };
            }).ToList();

            int totalCount = orderDTOs.Count;

            var paginatedDTOs = orderDTOs
                .Skip((@params.PageNumber - 1) * @params.PageSize)
                .Take(@params.PageSize)
                .ToList();

            return new PagedResult<OrderReadDTO>(paginatedDTOs, totalCount, @params.PageNumber, @params.PageSize);
        }


        public async Task<ICollection<OrderReadDTO>> GetAllAsync()
        {
            var orders = await _orderReadRepository.GetAllAsync(false, "Product");
            var campaigns = await _campaignReadRepository.GetAllAsync(false);

            return orders.Select(o =>
            {
                var activeCampaign = campaigns.FirstOrDefault(c =>
                    c.IsActive &&
                    c.StartDate <= o.CreatedAt &&
                    c.EndDate >= o.CreatedAt);

                return new OrderReadDTO
                {
                    Id = o.Id,
                    ProductId = o.ProductId,
                    ProductCount = o.ProductCount,
                    ProductTitle = o.Product.Title,
                    ProductPrice = o.Product.Price,
                    CampaignId = activeCampaign?.Id ?? 0,
                    CampaignName = activeCampaign?.Name ?? string.Empty,
                    TotalPrice = o.TotalPrice,
                    CreatedAt = o.CreatedAt
                };
            }).ToList();
        }



        Task<ICollection<OrderReadDTO>> IOrderService.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
