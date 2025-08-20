using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KDramaSystem.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KDramaDbContext>
    {
        public KDramaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<KDramaDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=kdrama_dev;Username=kdrama;Password=othaviogamex10",
                b => b.MigrationsAssembly(typeof(KDramaDbContext).Assembly.FullName)
            );

            return new KDramaDbContext(optionsBuilder.Options);
        }
    }
}