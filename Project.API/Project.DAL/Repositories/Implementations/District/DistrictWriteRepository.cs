using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.District;

namespace Project.DAL.Repositories.Implementations.District
{
    public class DistrictWriteRepository : WriteRepository<Core.Entities.District>, IDistrictWriteRepository
    {
        public DistrictWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
     
}
