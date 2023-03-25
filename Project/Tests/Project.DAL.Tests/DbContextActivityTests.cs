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
            Assert.Equal(1, activities);
        }
    }
}
