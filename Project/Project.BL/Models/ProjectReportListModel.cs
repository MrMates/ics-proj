using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ProjectReportListModel : ModelBase
    {
        public required string ProjectName { get; set; }
        
        public static ProjectReportListModel Empty => new()
        { 
            Id = Guid.NewGuid(),
            ProjectName = string.Empty,
        };

    }
}
