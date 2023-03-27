using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record UserListModel : ModelBase
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhotoUrl { get; set; }

        public static UserListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            FirstName = string.Empty,
            LastName = string.Empty,
            PhotoUrl = null
        };
    }
}
