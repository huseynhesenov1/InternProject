using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Order;

namespace Project.DAL.Repositories.Implementations.Order
{
    public class OrderWriteRepository : WriteRepository<Core.Entities.Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
     
}
