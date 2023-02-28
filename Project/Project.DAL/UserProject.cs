using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public class UserProject
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }

    }
}
