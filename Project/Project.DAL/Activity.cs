using System.ComponentModel.DataAnnotations;


namespace Project.DAL
{
    public class Activity
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid ProjectId { get; set; }
        public required Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime TimeEnd { get; set; }
        public string ActivityType { get; set; }
        public string ActivityDescription { get; set; }
    }
}
