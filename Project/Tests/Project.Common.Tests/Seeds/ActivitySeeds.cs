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
            Type = default!,
            ProjectId = default!,
            UserId = default!
        };

        public static readonly Activity WorkActivity = new()
        {
            Id = Guid.Parse("5AC46AA9-2DA3-4D1D-B89D-687850B76623"),
            Name = "Day 58394 of work",
            ActivityDescription = "so much fun...",
            Type = Enums.ActivityType.Work,
            TimeBegin = DateTime.Parse("2021-05-01 13:01:20"),
            TimeEnd = DateTime.Parse("2021-05-01 15:21:54"),
            ProjectId = ProjectSeeds.AgencyProject.Id,
            UserId = UserSeeds.DefaultUser.Id
        };

        public static readonly Activity ActivityToDelete = WorkActivity with { Id = Guid.Parse("A9336994-C6D9-482E-BEBD-40C7148E5384"), ProjectId = null };

        // NOT SEEDED ACTIVITIES ----------
        // Activities that are not inserted into the database and are here for filter testing convenience.

        public static readonly Activity NotSeededPastWeek = new()
        {
            Id = Guid.NewGuid(),
            Name = "Today's work",
            Type = Enums.ActivityType.Work,
            TimeBegin = DateTime.Today,
            UserId = UserSeeds.DefaultUser.Id,
            ProjectId = ProjectSeeds.AgencyProject.Id
        };

        public static readonly Activity NotSeededPastMonth = new ()
        {
            Id = Guid.NewGuid(),
            Name = "Work from a few weeks ago",
            Type = Enums.ActivityType.Work,
            TimeBegin = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
            UserId = UserSeeds.DefaultUser.Id,
            ProjectId = ProjectSeeds.AgencyProject.Id
        };

        public static readonly Activity NotSeededPreviousMonth = new()
        {
            Id = Guid.NewGuid(),
            Name = "Work from the previous month",
            Type = Enums.ActivityType.Work,
            TimeBegin = new DateTime(DateTime.Today.AddMonths(-1).Year, DateTime.Today.AddMonths(-1).Month, 1),
            UserId = UserSeeds.DefaultUser.Id,
            ProjectId = ProjectSeeds.AgencyProject.Id
        };

        public static readonly Activity NotSeededPastYear = new()
        {
            Id = Guid.NewGuid(),
            Name = "Work from the past year",
            Type = Enums.ActivityType.Work,
            TimeBegin = new DateTime(DateTime.Today.Year, 1, 1),
            UserId = UserSeeds.DefaultUser.Id,
            ProjectId = ProjectSeeds.AgencyProject.Id
        };

        public static readonly Activity NotSeededPreviousYear = new()
        {
            Id = Guid.NewGuid(),
            Name = "Work from the previous year",
            Type = Enums.ActivityType.Work,
            TimeBegin = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1),
            UserId = UserSeeds.DefaultUser.Id,
            ProjectId = ProjectSeeds.AgencyProject.Id
        };

        // END NOT SEEDED ACTIVITIES ------

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(
                WorkActivity,ActivityToDelete
            );
        }
    }
}
