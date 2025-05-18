using Microsoft.EntityFrameworkCore;
using Project.Core.Entities.Commons;

namespace Project.DAL.Repositories.Abstractions
{
    public interface IRepository<Tentity> where Tentity : BaseAuditableEntity, new()
    {
        public DbSet<Tentity> Table { get; }
    }
}
