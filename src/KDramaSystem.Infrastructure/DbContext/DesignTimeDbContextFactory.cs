using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KDramaSystem.Infrastructure.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KDramaDbContext>
    {
        public KDramaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<KDramaDbContext>();
            optionsBuilder.UseSqlite("Data Source=kdrama.db");

            return new KDramaDbContext(optionsBuilder.Options);
        }
    }
}