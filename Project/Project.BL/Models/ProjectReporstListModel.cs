using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ProjectReporstListModel : ModelBase
    {
        public required Guid ProjectId { get; set; }
        public required string ProjectName { get; set; }
        public TimeSpan TimeSpent { get; set; } 
        public TimeSpan TotalTimeSpent { get; set; }
        public decimal PercentageOfTimeSpent { get; set; }

        public static ProjectReporstListModel Empty => new()
        { 
            Id = Guid.NewGuid(),
            ProjectId = Guid.Empty,
            ProjectName = string.Empty,
        };

    }
}
