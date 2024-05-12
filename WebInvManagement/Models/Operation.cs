using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }

        // Many-to-many [Operation has ProductionStock]
        public ICollection<OperationProductionStock>? OperationProductionStocks { get; set; }
    }
}
