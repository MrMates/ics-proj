using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ActivityListModel : ModelBase
    {
        public Guid ProjectId { get; set; }
        public required string ActivityName { get; set; }

        public static ActivityListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ActivityName = string.Empty,
            ProjectId = Guid.Empty
        };
    }
}
