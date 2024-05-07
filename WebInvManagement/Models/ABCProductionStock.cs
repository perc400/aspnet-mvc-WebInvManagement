namespace WebInvManagement.Models
{
    public class ABCProductionStock
    {
        public int ABCId { get; set; }
        public ABCGroup ABCGroup { get; set; }

        public int ProductionStockId { get; set; }
        public ProductionStock ProductionStock { get; set; }
    }
}
