namespace WebInvManagement.Models
{
    public class XYZProductionStock
    {
        public int XYZId { get; set; }
        public XYZGroup XYZGroup { get; set; }

        public int ProductionStockId { get; set; }
        public ProductionStock ProductionStock { get; set; }
    }
}
