using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common.Tests;
using Project.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace Project.DAL.Tests
{
    public class DbContextActivityTests : DbContextTestsBase
    {

        public DbContextActivityTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task GetCount_Activity()
        {
            //Act
            var activities = await DbContextSUT.Activities
                .CountAsync();

            //Assert
            Assert.Equal(2, activities);
        }

        [Fact]
        public async Task CreateNew_Activity_Persisted()
        {
            //Arrange
            var entity = ActivitySeeds.EmptyActivity with
            {
                Name = "Name",
                ActivityType = "Type",
                UserId = UserSeeds.DefaultUser.Id,
                ProjectId = ProjectSeeds.AgencyProject.Id
            };

            //Act
            DbContextSUT.Activities.Add(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Activities
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Read_Activity_Successful()
        {
            //Arrange
            var entity = ActivitySeeds.WorkActivity;

            //Act
            var actualEntity = await DbContextSUT.Activities
                .SingleAsync(i => i.Id == entity.Id);

            //Assert
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Update_Activity_Changed()
        {
            //Arrange
            var unchanged = ActivitySeeds.WorkActivity;
            var entity =
                unchanged with
                {
                    Name = unchanged.Name + "Changed",
                    ActivityType = unchanged.ActivityType + "Changed",
                };

            //Act
            DbContextSUT.Activities.Update(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Activities
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_Activity_Deleted()
        {
            //Arrange
            var entity = ActivitySeeds.ActivityToDelete;

            //Act
            DbContextSUT.Activities.Remove(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await DbContextSUT.Activities.AnyAsync(i => i.Id == entity.Id));
        }
    }
}
