using Project.BL.DTOs.OrderDTOs;
using Project.BL.Models;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IOrderService
    {
        Task<ApiResponse<OrderCreateResponseDTO>> CreateAsync(OrderCreateDTO dto);
        Task<ICollection<OrderReadDTO>> GetAllAsync();
    }
}