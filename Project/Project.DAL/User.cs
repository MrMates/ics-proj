using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ActivityId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhotoUrl { get; set; }
    }
}
