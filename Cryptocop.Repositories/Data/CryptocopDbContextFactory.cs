using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cryptocop.Repositories.Data
{
    public class CryptocopDbContextFactory : IDesignTimeDbContextFactory<CryptocopDbContext>
    {
        public CryptocopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CryptocopDbContext>();
            
            const string connectionString =
                "Host=localhost;Port=5432;Database=cryptocop;Username=postgres;Password=postgres";

            optionsBuilder.UseNpgsql(connectionString);
            return new CryptocopDbContext(optionsBuilder.Options);
        }
    }
}