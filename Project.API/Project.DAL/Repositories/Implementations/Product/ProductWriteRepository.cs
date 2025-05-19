using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Product;

namespace Project.DAL.Repositories.Implementations.Product
{
   public  class ProductWriteRepository : WriteRepository<Core.Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
   
}
