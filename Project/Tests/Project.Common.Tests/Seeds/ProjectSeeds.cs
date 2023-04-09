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

        public static readonly DAL.Project ProjectToDelete = AgencyProject with { Id = Guid.Parse("A9336994-C6D9-482E-BEBD-40C7148E5384") };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DAL.Project>().HasData(
                AgencyProject with { Activities = Array.Empty<Activity>(), UserProjects = Array.Empty<UserProject>()},
                ProjectToDelete
            );
        }
    }
}
