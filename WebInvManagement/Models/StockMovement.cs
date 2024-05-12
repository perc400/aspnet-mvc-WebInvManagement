using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class StockMovement
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? Quantity { get; set; }

        // Many-to-many [StockMovement has ProductionStock]
        public ICollection<StockMovementProductionStock>? StockMovementProductionStocks { get; set; }
    }
}
