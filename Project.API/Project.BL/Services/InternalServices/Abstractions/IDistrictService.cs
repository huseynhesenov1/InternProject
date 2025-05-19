using Project.BL.DTOs.DistrictDTOs;
using Project.Core.Entities;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IDistrictService
    {
        Task<ICollection<DistrictReadDTO>> GetAllAsync();
        Task<District> GetByIdAsync(int id);
        Task<District> CreateAsync(DistrictCreateDTO studentCreateDTO);
        Task<District> UpdateAsync(int id, DistrictUpdateDTO studentUpdateDTO);
        Task<District> SoftDeleteAsync(int id);
    }
}
