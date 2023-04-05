using Project.Common.Tests.Seeds;
using Project.DAL;
using Microsoft.EntityFrameworkCore;

namespace Project.Common.Tests
{
    public class TestingDbContext : DatabaseContext
    {
        private readonly bool _seedTestingData;

        public TestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
            : base(contextOptions, seedDemoData:false)
        {
            _seedTestingData = seedTestingData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (_seedTestingData)
            {
                UserSeeds.Seed(modelBuilder);
                ActivitySeeds.Seed(modelBuilder);
                ProjectSeeds.Seed(modelBuilder);
            }
        }
    }
}
