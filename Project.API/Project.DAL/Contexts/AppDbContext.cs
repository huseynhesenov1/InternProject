using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {

        }
        public DbSet<Core.Entities.Worker> Workers { get; set; }
        public DbSet<Core.Entities.Product> Products { get; set; }
        public DbSet<Core.Entities.District> Districts { get; set; }
    }
}
