using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class ProductionStock
    {
        [Key]
        public int Id { get; set; }
        public int? Quantity { get; set; }
        //FK from StockType
        [ForeignKey("StockType")]
        public int? StockTypeId { get; set; }
        public StockType? StockType { get; set; }
        // FK from ABC
        [ForeignKey("ABCGroup")]
        public int? ABCId { get; set;}
        public ABCGroup? ABCGroup { get; set; }
        // FK from XYZ
        [ForeignKey("XYZGroup")]
        public int? XYZId { get; set; }
        public XYZGroup? XYZGroup { get; set; }
        // Many-to-many
        public ICollection<WarehouseProductionStock>? WarehouseProductionStocks { get; set; }

        public DateTime? LastUpdated { get; set; }
        public int? MinOrderQuantity { get; set; }
        public int? MaxOrderQuantity { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public string? ServiceLevel { get; set; }
        public string? LeadTime { get; set; }
    }
}
