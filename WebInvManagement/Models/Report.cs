using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class Report
    {
        [Key]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey("StockType")]
        public int? StockTypeId { get; set; }
        public StockType? StockType { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
