using Microsoft.EntityFrameworkCore;
using Project.DAL;


namespace Project.Common.Tests.Seeds
{
    public static class ActivitySeeds
    {
        public static readonly Activity EmptyActivity = new()
        {
            Id = default,
            Name = default!,
            ActivityType = default!,
            ProjectId = default!,
            UserId = default!
        };

        public static readonly Activity WorkActivity = new()
        {
            Id = Guid.Parse("5AC46AA9-2DA3-4D1D-B89D-687850B76623"),
            Name = "Day 58394 of work",
            ActivityDescription = "so much fun...",
            ActivityType = "Job",
            ProjectId = ProjectSeeds.AgencyProject.Id,
            UserId = UserSeeds.DefaultUser.Id
        };

        public static readonly Activity ActivityToDelete = WorkActivity with { Id = Guid.Parse("A9336994-C6D9-482E-BEBD-40C7148E5384") };


        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(
                WorkActivity,ActivityToDelete
            );
        }
    }
}
