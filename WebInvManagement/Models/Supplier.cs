using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class Supplier
    {
        [Key]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }

        [ForeignKey("StockType")]
        public int? StockTypeId { get; set; }
        public StockType? StockType { get; set; }
    }
}
