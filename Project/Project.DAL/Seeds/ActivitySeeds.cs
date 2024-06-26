﻿using Microsoft.EntityFrameworkCore;
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
            Type = Common.Enums.ActivityType.School,
            TimeBegin = DateTime.Now.AddHours(-1.2),
            TimeEnd = DateTime.Now.AddHours(-0.1),
        };

        public static readonly Activity DefaultActivity2 = new()
        {
            Id = Guid.Parse("188B8C5B-FFFF-452E-A20E-A111EB0CD21B"),
            UserId = Guid.Parse("188B8C5B-FCC8-452E-A20E-AF0DEB0CD21B"),
            ProjectId = Guid.Parse("188BFF5B-FCC8-452E-A20E-AF0DEB0DDD1B"),
            Name = "Recenze produktu",
            Type = Common.Enums.ActivityType.Work,
            ActivityDescription = "Tento balíček jsme vybrali protože obsahoval vesměs vše a byl to náš první nákup, první zkušenost, takže nedokážu moc porovnat kvalitu. Nevěděli jsme zda nám to bude vůbec vyhovovat. Zatím jsme zkusili pouze dvě věci a odhodláváme se na další. Myslím, že pro začátek a pro takové konzervy, jako jsme mi s manželem je to zatím akorát na to „zkoušení“.",
            TimeBegin = DateTime.Now.AddYears(-2),
            TimeEnd = DateTime.Now.AddHours(-24.1),
        };

        public static readonly Activity DefaultActivity3 = new()
        {
            Id = Guid.Parse("188B8C5B-FFFF-452E-A20E-A111EB0CD21C"),
            UserId = Guid.Parse("188B8C5B-FCC8-452E-A20E-AF0DEB0CD21B"),
            Name = "testování MAUI",
            Type = Common.Enums.ActivityType.Workout,
            TimeBegin = DateTime.Now.AddDays(-35),
            TimeEnd = DateTime.Now.AddDays(-25),
        };

        public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<Activity>().HasData(
                DefaultActivity,
                DefaultActivity2,
                DefaultActivity3
                );
    }


}
