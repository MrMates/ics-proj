using System;
using Project.DAL;
using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Seeds
{
    public static class ProjectSeeds
    {
        public static readonly Project DefaultProject = new()
        {
            Id = Guid.Parse("188B8C5B-FCC8-452E-A20E-AF0DEB0DDD1B"),
            Name = "ICS",
        };

        public static readonly Project DefaultProject2 = new()
        {
            Id = Guid.Parse("A357D7DF-9DF0-4787-B489-A970BBB34067"),
            Name = "IOS"
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<Project>().HasData(
                    DefaultProject with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    }
                );
    }
}
