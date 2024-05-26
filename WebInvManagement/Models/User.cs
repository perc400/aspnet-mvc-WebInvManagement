using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebInvManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        // FK from UserRole
        [ForeignKey("UserRole")]
        public int? RoleId { get; set; }
        public UserRole? Role { get; set; }
    }
}
