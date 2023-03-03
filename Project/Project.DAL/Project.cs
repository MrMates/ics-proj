using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public record Project : IEntity
    {
        [Key]
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<UserProject> UserProjects { get; init; } = new List<UserProject>();
        public ICollection<Activity> Activities { get; init; } = new List<Activity>();
    }
}
