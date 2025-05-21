using Project.BL.DTOs.ProductDTOs;
using Project.BL.Models;
using Project.Core.Entities.Commons;
using Project.Core.Entities;
using Project.BL.DTOs.CampaignDTOs;

namespace Project.BL.Services.InternalServices.Abstractions
{
   public interface ICampaignService
    {
        Task<ApiResponse<int>> CreateAsync(CampaignCreateDTO campaignCreateDTO);
        Task<ApiResponse<int>> UpdateAsync(int id, CampaignUpdateDTO campaignUpdateDTO);
        Task<ICollection<CampaignReadDTO>> GetAllAsync();
        Task<PagedResult<Campaign>> GetPaginatedAsync(PaginationParams @params);
        Task<ApiResponse<bool>> DeleteAsync(int id);
        Task<ApiResponse<bool>> EnableAsync(int id);
        Task<ApiResponse<bool>> DisableAsync(int id);
        Task<ApiResponse<CampaignReadDTO>> GetByIdAsync(int id);
    }
}
