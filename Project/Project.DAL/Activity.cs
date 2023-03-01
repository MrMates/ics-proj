using System.ComponentModel.DataAnnotations;


namespace Project.DAL
{
    public class Activity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime TimeBegin { get; set; }
        public DateTime TimeEnd { get; set; }
        public string ActivityType { get; set; }
        public string ActivityDescription { get; set; }
    }
}
