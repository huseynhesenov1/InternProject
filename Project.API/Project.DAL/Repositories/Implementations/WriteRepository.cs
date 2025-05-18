using Microsoft.EntityFrameworkCore;
using Project.Core.Entities.Commons;
using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions;

namespace Project.DAL.Repositories.Implementations
{
    public class WriteRepository<Tentity> : IWriteRepository<Tentity> where Tentity : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;

        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<Tentity> Table => _context.Set<Tentity>();

        public async Task<Tentity> CreateAsync(Tentity tentity)
        {
            await Table.AddAsync(tentity);
            return tentity;
        }

        public Tentity SoftDelete(Tentity tentity)
        {
            if (tentity is null)
            {
                throw new Exception("Bu Id ye uygun deyer tapilmadi");
            }
            tentity.IsDeleted = true;
            return tentity;

        }
        public Tentity HardDelete(Tentity tentity)
        {
            if (tentity is null)
            {
                throw new Exception("Bu Id ye uygun deyer tapilmadi");
            }
            Table.Remove(tentity);
            return tentity;

        }

        public Tentity Restore(Tentity tentity)
        {
            if (tentity is null)
            {
                throw new Exception("Bu Id ye uygun deyer tapilmadi");
            }
            tentity.IsDeleted = false;
            return tentity;
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();

        }

        public Tentity Update(Tentity tentity)
        {
            Table.Update(tentity);
            return tentity;
        }
    }
}
