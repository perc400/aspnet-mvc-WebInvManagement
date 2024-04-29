using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class XYZGroup
    {
        [Key]
        public int? Id { get; set; }
        public string? Group { get; set; }
        public string? Description { get; set; }

        [ForeignKey("IMStrategy")]
        public int? StrategyId { get; set; }
        public IMStrategy? Strategy { get; set; }
    }
}
