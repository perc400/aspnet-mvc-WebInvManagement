using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebInvManagement.Models
{
    public class ABCGroup
    {
        [Key]
        public int? Id { get; set; }
        public string? Group {  get; set; }
        public string? Description { get; set; }
            
        [ForeignKey("IMStrategy")]
        public int? StrategyId { get; set; }
        public IMStrategy? Strategy { get; set; }

        // Many-to-many
        public ICollection<ABCProductionStock>? ABCProductionStocks { get; set; }
    }
}
