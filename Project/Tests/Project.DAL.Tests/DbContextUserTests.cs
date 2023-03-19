using Microsoft.EntityFrameworkCore;
using Project.Common.Tests;
using Project.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace Project.DAL.Tests
{
    public class DbContextUserTests : DbContextTestsBase
    {

        public DbContextUserTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task AddNew_User_Persisted()
        {
            //Arrange
            var entity = UserSeeds.EmptyUser with
            {
                FirstName = "Tomáš",
                LastName = "Jedno",
            };

            //Act
            DbContextSUT.Users.Add(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_User_Deleted()
        {
            //Arrange
            var entity = UserSeeds.UserToDelete;

            //Act
            DbContextSUT.Users.Remove(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await DbContextSUT.Users.AnyAsync(i => i.Id == entity.Id));
        }

        [Fact]
        public async Task Update_User_Changed()
        {
            //Arrange
            var unchanged = UserSeeds.DefaultUser;
            var entity =
                unchanged with
                {
                    FirstName = unchanged.FirstName + "Changed",
                    LastName = unchanged.LastName + "Changed",
                    PhotoUrl = "none"
                };

            //Act
            DbContextSUT.Users.Update(entity);
            await DbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task GetById_User()
        {
            //Act
            var entity = await DbContextSUT.Users
                .SingleAsync(i => i.Id == UserSeeds.DefaultUser.Id);

            //Assert
            DeepAssert.Equal(
                UserSeeds.DefaultUser with { UserProjects = Array.Empty<UserProject>(), Activities = Array.Empty<Activity>() }, entity);
        }
    }
}
