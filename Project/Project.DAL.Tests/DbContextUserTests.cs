using Microsoft.EntityFrameworkCore;
using Project.Common.Tests;
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
            var entity = new User
            {
                Id = Guid.Parse("03D3AC6D-B95B-41D2-A94A-A8E6ECAE62DD"),
                FirstName = "Jan",
                LastName = "Novák"
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
    }
}
