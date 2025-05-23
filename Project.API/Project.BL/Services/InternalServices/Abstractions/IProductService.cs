using Project.BL.DTOs.ProductDTOs;
using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.Core.Entities;
using Project.Core.Entities.Commons;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IProductService
    {
        Task<int> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<int> UpdateAsync(int Id ,ProductUpdateDTO productUpdateDTO);
        Task<ICollection<ProductReadDTO>> GetAllAsync();
      
        Task<PagedResult<Product>> GetPaginatedAsync(PaginationParams @params);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<ProductReadDTO>> GetByIdAsync(int id);
        Task<ICollection<ProductReadDTO>> SearchProductsAsync(string title);

    }
}
