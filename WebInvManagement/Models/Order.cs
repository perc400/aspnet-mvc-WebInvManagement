using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class Order
    {
        [Key]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? Quantity { get; set; }

        [ForeignKey("StockType")]
        public int? StockTypeId { get; set; }
        public StockType? StockType { get; set; }
    }
}
