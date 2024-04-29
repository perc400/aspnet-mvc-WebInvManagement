namespace WebInvManagement.Models
{
    public class WarehouseProductionStock
    {
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int ProductionStockId { get; set; }
        public ProductionStock ProductionStock { get; set; }
    }
}
