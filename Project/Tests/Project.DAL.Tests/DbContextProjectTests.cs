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
        public async Task GetCount_Project()
        {
            //Act
            var projects = await DbContextSUT.Projects
                .CountAsync();

            //Assert
            Assert.Equal(2, projects);
        }

        [Fact]
        public async Task CreateNew_Project_Persisted()
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

        [Fact]
        public async Task Read_Project_Successful()
        {
            //Arrange
            var entity = ProjectSeeds.AgencyProject;

            //Act
            var actualEntity = await DbContextSUT.Projects
                .SingleAsync(i => i.Id == entity.Id);

            //Assert
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Update_Project_Changed()
        {
            //Arrange
            var unchanged = ProjectSeeds.AgencyProject;
            var entity =
                unchanged with
                {
                    Name = unchanged.Name + "Changed",
                };

            //Act
            DbContextSUT.Projects.Update(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Projects
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_Project_Deleted()
        {
            //Arrange
            var entity = ProjectSeeds.ProjectToDelete;

            //Acts
            DbContextSUT.Projects.Remove(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await DbContextSUT.Projects.AnyAsync(i => i.Id == entity.Id));
        }

        [Fact]
        public async Task Add_Activity_to_Project_Persisted()
        {
            //Arrange
            var projectEntity = ProjectSeeds.EmptyProject with
            {
                Name = "Project"
            };
            var activityEntity = ActivitySeeds.EmptyActivity with
            {
                Name = "Activity",
                Type = Common.Enums.ActivityType.Work,
                UserId = UserSeeds.DefaultUser.Id
            };
            projectEntity.Activities.Add(activityEntity);

            //Act
            DbContextSUT.Projects.Add(projectEntity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualProjectEntity = await dbx.Projects
                .Include(p => p.Activities)
                .SingleAsync(p => p.Id == projectEntity.Id);
            var actualActivityEntity = actualProjectEntity.Activities.Single(a => a.Id == activityEntity.Id);
            DeepAssert.Equal(projectEntity, actualProjectEntity);
            DeepAssert.Equal(activityEntity, actualActivityEntity);
        }
    }
}
