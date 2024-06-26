﻿using Project.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ActivityListModel : ModelBase
    {
        public Guid? ProjectId { get; set; }
        public required string ActivityName { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime? TimeEnd { get; set; }

        public TimeSpan TimeSpent { get; set; }
        public TimeSpan TotalTime { get; set; }
        public double Percentage { get; set; }
        public ActivityType Type { get; set; }
        public static ActivityListModel Empty => new()
        {
            Id = Guid.Empty,
            ActivityName = string.Empty,
            ProjectId = Guid.Empty,
            TimeBegin = DateTime.Now
        };
    }
}
