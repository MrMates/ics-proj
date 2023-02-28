using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
