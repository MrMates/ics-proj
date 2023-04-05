using Project.DAL;
using Microsoft.EntityFrameworkCore;

namespace Project.Common.Tests
{
    public class DbContextTestingFactory : IDbContextFactory<DatabaseContext>
    {
        private readonly string _databaseName;
        private readonly bool _seedTestingData;

        public DbContextTestingFactory(string databaseName, bool seedTestingData = false)
        {
            _databaseName = databaseName;
            _seedTestingData = seedTestingData;
        }

        public DatabaseContext CreateDbContext()
        {
            DbContextOptionsBuilder<DatabaseContext> builder = new();
            builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

            return new TestingDbContext(builder.Options, _seedTestingData);
        }
    }
}