using Project.BL.DTOs.OrderDTOs;
using Project.BL.Models;
using Project.Core.Entities.Commons;
using Project.Core.Entities;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IOrderService
    {
        Task<ApiResponse<OrderCreateResponseDTO>> CreateAsync(OrderCreateDTO dto);
        Task<ICollection<OrderReadDTO>> GetAllAsync();
        Task<PagedResult<OrderReadDTO>> GetPaginatedAsync(PaginationParams @params);

    }
}