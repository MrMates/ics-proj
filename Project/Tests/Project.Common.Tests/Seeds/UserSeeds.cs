using Microsoft.EntityFrameworkCore;
using Project.DAL;

namespace Project.Common.Tests.Seeds
{
    public static class UserSeeds
    {
        public static readonly User EmptyUser = new()
        {
            Id = default,
            FirstName = default!,
            LastName = default!,
            PhotoUrl = default
        };

        public static readonly User DefaultUser = new()
        {
            Id = Guid.Parse("03D3AC6D-B95B-41D2-A94A-A8E6ECAE62DD"),
            FirstName = "Jan",
            LastName = "Novák",
            PhotoUrl = "https://w7.pngwing.com/pngs/753/432/png-transparent-user-profile-2018-in-sight-user-conference-expo-business-default-business-angle-service-people-thumbnail.png",
        };

        public static readonly User UserToDelete = DefaultUser with { Id = Guid.Parse("A9336994-C6D9-482E-BEBD-40C7148E5384"), Activities = Array.Empty<Activity>(), UserProjects = Array.Empty<UserProject>() };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                DefaultUser with { Activities = Array.Empty<Activity>(), UserProjects = Array.Empty<UserProject>() },
                UserToDelete
                );
        }
    }
}
