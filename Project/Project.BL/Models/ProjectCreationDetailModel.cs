using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ProjectCreationDetailModel : ModelBase
    {
        public required string ProjectName { get; set; }

        public static ProjectCreationDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectName = string.Empty
        };
    }
}
