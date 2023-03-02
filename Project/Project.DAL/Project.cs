using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public class Project
    {
        [Key]
        public required Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; init; } = new List<User>();
        public ICollection<UserProject> UserProjects { get; init; } = new List<UserProject>();
        public ICollection<Activity> Activities { get; init; } = new List<Activity>();
    }
}
