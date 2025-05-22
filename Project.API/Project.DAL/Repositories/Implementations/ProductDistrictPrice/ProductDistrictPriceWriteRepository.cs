using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions;

namespace Project.DAL.Repositories.Implementations.ProductDistrictPrice
{
    public class ProductDistrictPriceWriteRepository : WriteRepository<Project.Core.Entities.ProductDistrictPrice>, IProductDistrictPriceWriteRepository
    {
        public ProductDistrictPriceWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
