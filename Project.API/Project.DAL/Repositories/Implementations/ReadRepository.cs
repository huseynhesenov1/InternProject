using Microsoft.EntityFrameworkCore;
using Project.Core.Entities.Commons;
using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions;

namespace Project.DAL.Repositories.Implementations
{
    public class ReadRepository<Tentity> : IReadRepository<Tentity> where Tentity : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<Tentity> Table => _context.Set<Tentity>();



        public async Task<Tentity> GetByIdAsync(int id, bool isTracking, params string[] includes)
        {
            IQueryable<Tentity> query = Table.AsQueryable();
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            if (includes.Length > 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            Tentity? tentity = await query.FirstOrDefaultAsync(x => x.Id == id);
            return tentity;

        }

        public async Task<ICollection<Tentity>> GetAllAsync(bool deleted, params string[] includes)
        {
            IQueryable<Tentity> query = Table.AsQueryable();
            if (includes.Length > 0)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.Where(x => x.IsDeleted == deleted).ToListAsync();
        }
    }
}
