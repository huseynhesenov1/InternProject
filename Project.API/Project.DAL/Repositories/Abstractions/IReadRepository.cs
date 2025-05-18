using Project.Core.Entities.Commons;

namespace Project.DAL.Repositories.Abstractions
{
    public interface IReadRepository<Tentity> : IRepository<Tentity> where Tentity : BaseAuditableEntity, new()
    {
        Task<Tentity> GetByIdAsync(int id, bool isTracking, params string[] includes);
        Task<ICollection<Tentity>> GetAllAsync(bool deleted, params string[] includes);
    }
}
