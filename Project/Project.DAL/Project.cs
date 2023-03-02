using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public class Project
    {
        [Key]
        public required Guid Id { get; set; }
        public string Name { get; set; }
    }
}
