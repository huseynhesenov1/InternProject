using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.District;

namespace Project.DAL.Repositories.Implementations.District
{
    public class DistrictReadRepository : ReadRepository<Core.Entities.District>, IDistrictReadRepository
    {
        public DistrictReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
