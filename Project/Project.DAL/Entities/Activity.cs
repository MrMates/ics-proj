using System.ComponentModel.DataAnnotations;


namespace Project.DAL
{
    public record Activity : IEntity
    {
        public required Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public required Guid UserId { get; set; }
        //public Project? Project { get; init; }
        //public User? User { get; init; }
        public required string Name { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime? TimeEnd { get; set; } // activity doesnt have to have end time
        public required string ActivityType { get; set; }
        public string? ActivityDescription { get; set; }
    }
}
