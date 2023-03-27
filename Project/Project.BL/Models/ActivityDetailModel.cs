using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ActivityDetailModel : ModelBase
    {
        public Guid ProjectId { get; set; }
        public required string ActivityName { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime? TimeEnd { get; set; } // activity doesnt have to have end time
        public required string ActivityType { get; set; }
        public string? ActivityDescription { get; set; }

        public static ActivityDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectId = Guid.Empty,
            ActivityName = string.Empty,
            ActivityType = string.Empty,
            TimeBegin =DateTime.Now
        };
    }
}
