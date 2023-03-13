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
    }
}
