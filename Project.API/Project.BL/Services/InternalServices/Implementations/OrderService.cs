using Project.BL.DTOs.OrderDTOs;
using Project.BL.Models;
using Project.BL.Services.InternalServices.Abstractions;
using Project.DAL.Repositories.Abstractions.Campaign;
using Project.DAL.Repositories.Abstractions.Order;
using Project.DAL.Repositories.Abstractions.Product;
using Project.DAL.Repositories.Abstractions.Worker;
using Microsoft.AspNetCore.Http;
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

        public Task<ApiResponse<OrderCreateResponseDTO>> CreateAsync(OrderCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<OrderReadDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
