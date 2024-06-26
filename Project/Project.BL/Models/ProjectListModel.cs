﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Models
{
    public record ProjectListModel : ModelBase
    {
        public required string ProjectName { get; set; }
        public ICollection<UserListModel>? Users { get; set; }

        public TimeSpan TimeSpent { get; set; }

        public static ProjectListModel empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectName = string.Empty,
        };
    }
}
