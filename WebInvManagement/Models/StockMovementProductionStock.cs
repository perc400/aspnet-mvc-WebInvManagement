namespace WebInvManagement.Models
{
    public class StockMovementProductionStock
    {
        public int StockMovementId { get; set; }
        public StockMovement StockMovement { get; set; }

        public int ProductionStockId { get; set; }
        public ProductionStock ProductionStock { get; set; }
    }
}
