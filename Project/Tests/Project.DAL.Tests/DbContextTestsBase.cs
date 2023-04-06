using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Project.Common.Tests;
using Xunit;
using Xunit.Abstractions;

namespace Project.DAL.Tests
{
    public class DbContextTestsBase : IAsyncLifetime
    {
        protected IDbContextFactory<DatabaseContext> DbContextFactory { get; }
        protected DatabaseContext DbContextSUT { get; }

        protected DbContextTestsBase(ITestOutputHelper output)
        {
            DbContextFactory = new DbContextTestingFactory(GetType().FullName!, seedTestingData: true);
            DbContextSUT = DbContextFactory.CreateDbContext();
        }

        public async Task InitializeAsync()
        {
            await DbContextSUT.Database.EnsureDeletedAsync();
            await DbContextSUT.Database.EnsureCreatedAsync();
        }

        public async Task DisposeAsync()
        {
            await DbContextSUT.Database.EnsureDeletedAsync();
            await DbContextSUT.DisposeAsync();
        }
    }
}