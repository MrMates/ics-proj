using System;
using Project.DAL;
using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Seeds
{
    public static class UserSeeds
    {
        public static readonly User DefaultUser = new()
        {
            Id = Guid.Parse("188B8C5B-FCC8-452E-A20E-AF0DEB0CD21B"),
            FirstName = "Lukáš",
            LastName = "Etzler",
            PhotoUrl = 
                @"https://w7.pngwing.com/pngs/753/432/png-transparent-user-profile-2018-in-sight-user-conference-expo-business-default-business-angle-service-people-thumbnail.png"
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<User>().HasData(
                    DefaultUser with { 
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    }
                );
    }
}
