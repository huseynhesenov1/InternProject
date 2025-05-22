using Project.BL.DTOs.ProductDistrictPriceDTOs;
using Project.BL.Services.InternalServices.Abstractions;
using Project.Core.Entities;
using Project.DAL.Repositories.Abstractions;
using Project.DAL.Repositories.Abstractions.ProductDistrictPrice;

namespace Project.BL.Services.InternalServices.Implementations
{
    public class ProductDistrictService : IProductDistrictService
    {
        private readonly IProductDistrictPriceReadRepository _productDistrictPriceReadRepository;
        private readonly IProductDistrictPriceWriteRepository _productDistrictPriceWriteRepository;

        public ProductDistrictService(IProductDistrictPriceWriteRepository productDistrictPriceWriteRepository, IProductDistrictPriceReadRepository productDistrictPriceReadRepository)
        {
            _productDistrictPriceWriteRepository = productDistrictPriceWriteRepository;
            _productDistrictPriceReadRepository = productDistrictPriceReadRepository;
        }

        public async Task<ProductDistrictPrice> CreateAsync(ProductDistrictPriceCreateDTO dto)
        {
            ProductDistrictPrice productDistrictPrice = new ProductDistrictPrice()
            {
                Price = dto.Price,
                ProductId = dto.ProductId,
                DistrictId = dto.DistrictId,
            };
            productDistrictPrice.CreatedAt = DateTime.UtcNow.AddHours(4);
            var res = await _productDistrictPriceWriteRepository.CreateAsync(productDistrictPrice);
            await _productDistrictPriceWriteRepository.SaveChangeAsync();
            return res;
        }

        public async Task<ICollection<ProductDistrictPrice>> GetAllAsync()
        {
            ICollection<ProductDistrictPrice> result = await _productDistrictPriceReadRepository.GetAllAsync(false);
            return result;
        }
    }
}
