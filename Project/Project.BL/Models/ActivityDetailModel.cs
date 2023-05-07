using Project.Common.Enums;

namespace Project.BL.Models
{
    public record ActivityDetailModel : ModelBase
    {
        public Guid? ProjectId { get; set; }
        public Guid UserId { get; set; }
        public required string ActivityName { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime? TimeEnd { get; set; }
        public required ActivityType Type { get; set; }
        public string? ActivityDescription { get; set; }

        public static ActivityDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            ProjectId = Guid.Empty,
            UserId = Guid.Empty,
            ActivityName = string.Empty,
            Type = ActivityType.Other,
            TimeBegin =DateTime.Now
        };
    }
}
