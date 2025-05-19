using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Product;

namespace Project.DAL.Repositories.Implementations.Product
{
    public class ProductReadRepository : ReadRepository<Core.Entities.Product>, IProductReadRepository
    {
        public ProductReadRepository(AppDbContext context) : base(context)
        {
        }
    }
     
}
