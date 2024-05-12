namespace WebInvManagement.Models
{
    public class OperationProductionStock
    {
        public int OperationId { get; set; }
        public Operation Operation { get; set; }

        public int ProductionStockId { get; set; }
        public ProductionStock ProductionStock { get; set; }
    }
}
