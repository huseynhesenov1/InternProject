using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Worker;

namespace Project.DAL.Repositories.Implementations.Worker
{
    public class WorkerWriteRepository : WriteRepository<Core.Entities.Worker>, IWorkerWriteRepository
    {
        public WorkerWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
