using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class IMStrategy
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
