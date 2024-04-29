using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebInvManagement.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        // Many-to-many
        public ICollection<WarehouseProductionStock>? WarehouseProductionStocks { get; set; }
    }
}
