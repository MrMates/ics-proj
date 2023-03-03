using System.ComponentModel.DataAnnotations;

namespace Project.DAL
{
    public record UserProject : IEntity
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ProjectId { get; set; }
        public required User User { get; set; }
        public required Project Project { get; set; }
    }
}
