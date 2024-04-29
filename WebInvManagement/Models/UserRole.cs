using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}
