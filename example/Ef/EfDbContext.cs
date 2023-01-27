using Microsoft.EntityFrameworkCore;

namespace Byndyusoft.Data.Sessions.Example.Ef
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<People> Peoples { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfDbContext).Assembly);
        }
    }
}