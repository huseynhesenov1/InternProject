using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.ProductDistrictPrice;

namespace Project.DAL.Repositories.Implementations.ProductDistrictPrice
{
    public class ProductDistrictPriceReadRepository : ReadRepository<Project.Core.Entities.ProductDistrictPrice>, IProductDistrictPriceReadRepository
    {
        public ProductDistrictPriceReadRepository(AppDbContext context) : base(context)
        {
        }
    }
     
}
