using Microsoft.EntityFrameworkCore;
using Project.DAL;


namespace Project.Common.Tests.Seeds
{
    public static class ProjectSeeds
    {
        public static readonly DAL.Project EmptyProject = new()
        {
            Id = default,
            Name = default!
        };

        public static readonly DAL.Project AgencyProject = new()
        {
            Id = Guid.Parse("DFC4CE68-72D1-4B88-92C2-CD826900636B"),
            Name = "Real Agency"
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DAL.Project>().HasData(
                AgencyProject with { Activities = Array.Empty<Activity>(), UserProjects = Array.Empty<UserProject>()}
            );
        }
    }
}
