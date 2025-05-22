using Project.BL.DTOs.ProductDistrictPriceDTOs;
using Project.Core.Entities;

namespace Project.BL.Services.InternalServices.Abstractions
{
    public interface IProductDistrictService
    {
        Task<ICollection<ProductDistrictPrice>> GetAllAsync();
        Task<ProductDistrictPrice> CreateAsync(ProductDistrictPriceCreateDTO dto);
    }
}
