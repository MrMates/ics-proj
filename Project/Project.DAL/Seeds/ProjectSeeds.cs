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
            Name = "Středa meeting",
        };

        public static readonly Project DefaultProject2 = new()
        {
            Id = Guid.Parse("188B8C5B-FCC8-452E-A20E-AF0DEB0DDD12"),
            Name = "Obhajoba"
        };
        public static readonly Project DefaultProject3 = new()
        {
            Id = Guid.Parse("1D8B8C5B-FCC8-452E-A20E-AF0DEB0DDD1B"),
            Name = "Zkoušky"
        };
        public static readonly Project DefaultProject4 = new()
        {
            Id = Guid.Parse("188B8C5B-FCC8-452E-AB0E-AF0DEB0DDD1B"),
            Name = "Oznuk"
        };
        public static readonly Project DefaultProject5 = new()
        {
            Id = Guid.Parse("188B8C5B-FCC8-452E-A20E-AFDDEB0DDD1B"),
            Name = "Úřad práce"
        };
        public static readonly Project DefaultProject6 = new()
        {
            Id = Guid.Parse("188F8C5B-FCC8-452E-A20E-AF0DEB0DDD1B"),
            Name = "Land a job"
        };
        public static readonly Project DefaultProject7 = new()
        {
            Id = Guid.Parse("188BFF5B-FCC8-452E-A20E-AF0DEB0DDD1B"),
            Name = "Profit"
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<Project>().HasData(
                    DefaultProject with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    },
                    DefaultProject2 with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    },
                    DefaultProject3 with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    },
                    DefaultProject4 with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    },
                    DefaultProject5 with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    },
                    DefaultProject6 with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    },
                    DefaultProject7 with
                    {
                        UserProjects = Array.Empty<UserProject>(),
                        Activities = Array.Empty<Activity>(),
                    }
                );
    }
}
