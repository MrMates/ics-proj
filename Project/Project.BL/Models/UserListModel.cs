using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record UserListModel : ModelBase
    {
        public required string UserFirstName { get; set; }
        public required string UserLastName { get; set; }
        public string? UserPhotoUrl { get; set; }

        public static UserListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            UserFirstName = string.Empty,
            UserLastName = string.Empty,
            UserPhotoUrl = null
        };
    }
}
