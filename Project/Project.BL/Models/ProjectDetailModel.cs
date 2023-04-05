using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ProjectDetailModel : ModelBase
    {
        public TimeSpan TimeSpent { get; set; }
        public ObservableCollection<UserListModel> Users { get; init; } = new();
        public ObservableCollection<ActivityListModel> Activities { get; init; } = new();
        public static ProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            TimeSpent = TimeSpan.Zero
        };
    }
}
