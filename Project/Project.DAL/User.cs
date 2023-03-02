using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public class User
    {
        [Key]
        public required Guid Id { get; set; }

        public required string FirstName { get; set; }
       
        public required string LastName { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<Activity> Activities { get; init; } = new List<Activity>();

        public ICollection<UserProject> UserProjects { get; init; } = new List<UserProject>();
        
    }
}
