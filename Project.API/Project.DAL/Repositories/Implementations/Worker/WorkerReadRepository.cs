using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Worker;

namespace Project.DAL.Repositories.Implementations.Worker
{
    public class WorkerReadRepository : ReadRepository<Core.Entities.Worker>, IWorkerReadRepository
    {
        public WorkerReadRepository(AppDbContext context) : base(context)
        {
        }
    }
   
}
