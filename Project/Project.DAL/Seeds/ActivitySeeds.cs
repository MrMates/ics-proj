using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Seeds
{
    public static class ActivitySeeds
    {
        public static readonly Activity DefaultActivity = new()
        {
            Id = Guid.Parse("188B8C5B-FFFF-452E-A20E-AF0DEB0CD21B"),
            UserId = Guid.Parse("188B8C5B-FCC8-452E-A20E-AF0DEB0CD21B"),
            Name = "MAUI Finalization",
            ActivityType = "Škola"
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<Activity>().HasData(
                DefaultActivity

                );
    }


}
