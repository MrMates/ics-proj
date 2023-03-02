using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public class UserProject
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }

        

    }
}
