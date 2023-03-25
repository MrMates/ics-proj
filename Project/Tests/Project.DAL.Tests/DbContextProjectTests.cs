using Microsoft.EntityFrameworkCore;
using Project.Common.Tests;
using Project.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace Project.DAL.Tests
{
    public class DbContextProjectTests : DbContextTestsBase
    {

        public DbContextProjectTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task AddNew_Project_Persisted()
        {
            //Arrange
            var entity = ProjectSeeds.EmptyProject with
            {
                Name = "test"
            };

            //Act
            DbContextSUT.Projects.Add(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Projects
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }
    }
}
