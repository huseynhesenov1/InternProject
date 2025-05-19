using Project.BL.DTOs.ProductDTOs;
using Project.BL.DTOs.WorkerDTOs;
using Project.BL.Models;
using Project.BL.Services.InternalServices.Abstractions;
using Project.Core.Entities;
using Project.Core.Entities.Commons;
using Project.DAL.Repositories.Abstractions.Product;

namespace Project.BL.Services.InternalServices.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<int> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            Product product = new Product()
            {
                Price = productCreateDTO.Price,
                Title = productCreateDTO.Title,
            };
            product.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _productWriteRepository.CreateAsync(product);
            await _productWriteRepository.SaveChangeAsync();
            return product.Id;
        }

        public async Task<int> UpdateAsync(int Id, ProductUpdateDTO productUpdateDTO)
        {
            Product product = await _productReadRepository.GetByIdAsync(Id, false);
            if (product == null)
            {
                throw new Exception("Invalid ID");
            }
            Product newProduct = new Product()
            {
                Price = productUpdateDTO.Price,
                Title = productUpdateDTO.Title,
            };
            newProduct.Id = Id;
            newProduct.UpdatedAt = DateTime.UtcNow.AddHours(4);
            _productWriteRepository.Update(newProduct);
            await _productWriteRepository.SaveChangeAsync();
            return newProduct.Id;
        }

        public async Task<ICollection<ProductReadDTO>> GetAllAsync()
        {
            ICollection<Product> products = await _productReadRepository.GetAllAsync(false);
            List<ProductReadDTO> productReadDTOs = products
        .Select(p => new ProductReadDTO
        {
            Id = p.Id,
            Title = p.Title,
            Price = p.Price,
            OldPrice = p.Price,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,

        }).ToList();
            return productReadDTOs;
        }

        public async Task<PagedResult<Product>> GetPaginatedAsync(PaginationParams @params)
        {
            var allCategories = await _productReadRepository.GetAllAsync(false);

            var filtered = allCategories
                .Skip((@params.PageNumber - 1) * @params.PageSize)
                .Take(@params.PageSize)
                .ToList();
            int totalCount = allCategories.Count;
            return new PagedResult<Product>(filtered, totalCount, @params.PageNumber, @params.PageSize);
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var worker = await _productReadRepository.GetByIdAsync(id, true);
                if (worker == null || worker.IsDeleted)
                {
                    return ApiResponse<bool>.Fail("Product not found", "Invalid Product ID");
                }

                _productWriteRepository.SoftDelete(worker);
                await _productWriteRepository.SaveChangeAsync();

                return ApiResponse<bool>.Success(true, "Product deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message, "Error deleting Product");
            }
        }

        public async Task<ApiResponse<ProductReadDTO>> GetByIdAsync(int id)
        {
            try
            {
                var product = await _productReadRepository.GetByIdAsync(id, false);
                if (product == null || product.IsDeleted)
                {
                    return ApiResponse<ProductReadDTO>.Fail("Product not found", "Invalid Product ID");
                }
                ProductReadDTO productReadDTO = new ProductReadDTO()
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    OldPrice = product.Price,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,

                };

                return ApiResponse<ProductReadDTO>.Success(productReadDTO, "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductReadDTO>.Fail(ex.Message, "Error retrieving Product");
            }
        }

        public async Task<ICollection<ProductReadDTO>> SearchProductsAsync(string title)
        {
            var query = await _productReadRepository.GetAllAsync(false);

            query = query
            .Where(p =>
            (string.IsNullOrWhiteSpace(title) ||
             p.Title.Contains(title, StringComparison.OrdinalIgnoreCase))).ToList();
            var workerDTOs = query.Select(p => new ProductReadDTO
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                OldPrice = p.Price,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,

            }).ToList();
            return workerDTOs;
        }


    }
}
