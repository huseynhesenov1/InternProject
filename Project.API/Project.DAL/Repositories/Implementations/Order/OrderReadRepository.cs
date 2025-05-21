using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Order;

namespace Project.DAL.Repositories.Implementations.Order
{
    public class OrderReadRepository : ReadRepository<Core.Entities.Order>, IOrderReadRepository
    {
        public OrderReadRepository(AppDbContext context) : base(context)
        {
        }
    }
     
}
