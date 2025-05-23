using Project.BL.DTOs.OrderDTOs;
using Project.BL.Models;
using Project.BL.Services.InternalServices.Abstractions;
using Project.DAL.Repositories.Abstractions.Campaign;
using Project.DAL.Repositories.Abstractions.Order;
using Project.DAL.Repositories.Abstractions.Product;
using Project.DAL.Repositories.Abstractions.Worker;
using Microsoft.AspNetCore.Http;
using Project.Core.Entities;
using System.Security.Claims;
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

                // Aktiv kampaniyanı tap
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

                // Rayon üzrə qiymət endirimi tətbiq et
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





        public async Task<ICollection<OrderReadDTO>> GetAllAsync()
        {
            ICollection<Order> orders = await _orderReadRepository.GetAllAsync(false, "Product", "Campaign");

            return orders.Select(o => new OrderReadDTO
            {
                Id = o.Id,
                ProductId = o.ProductId,
                ProductCount = o.ProductCount,
                ProductTitle = o.Product.Title,
                ProductPrice = o.Product.Price,
                CampaignId = o.Product.CampaignId ?? 0,
                CampaignName = o.Product.Campaign?.Name ?? string.Empty,
                TotalPrice = o.TotalPrice,
                CreatedAt = o.CreatedAt
            }).ToList();
        }
    }
}
