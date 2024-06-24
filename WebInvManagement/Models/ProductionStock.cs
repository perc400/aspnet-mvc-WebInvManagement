using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebInvManagement.Models
{
    public class ProductionStock
    {
        [Key]
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public string? Title { get; set; }

        //FK from StockType
        [ForeignKey("StockType")]
        public int? StockTypeId { get; set; }
        public StockType? StockType { get; set; }
        
        // Many-to-many [Warehouse has Stock]
        public ICollection<WarehouseProductionStock>? WarehouseProductionStocks { get; set; }
        // Many-to-many [ABC has Stock]
        public ICollection<ABCProductionStock>? ABCProductionStocks { get; set; }
        // Many-to-many [XYZ has Stock]
        public ICollection<XYZProductionStock>? XYZProductionStocks { get; set; }

        // Поля для системы управления запасами
        public int? MaxDesiredLevel { get; set; } // максимальный желаемый уровень запасов
        public int? OptimalOrderSize { get; set; } // оптимальный размер заказа
        public int? DailyConsumption { get; set; } // дневное потребление товара на складе
        public int? SafetyStock { get; set; } // гарантийный запас на складе
        public int? ExpectedConsumptionDuringLeadTime { get; set; } // ожидаемое потребление товара на складе за время выполнения заказа
        public int? ReorderPoint { get; set; } // пороговый уровень запасов на складе
        public int? MaximumDesirableStockLevel { get; set; } // максимальный желательный уровень запасов на складе
        public double? CarryingCostPerOrder { get; set; } // транспортные расходы на выполнение одного заказа
        public double? HoldingCostPerUnitPerYear { get; set; } // издержки на хранение единицы товара
        public double? AnnualDemand { get; set; } // годовой спрос
        public int? Cost { get; set; } // стоимость есдиницы товара

        // Many-to-many [StockMovement has ProductionStock]
        public ICollection<StockMovementProductionStock>? StockMovementProductionStocks { get; set; }

        // Many-to-many [Operation has ProductionStock]
        public ICollection<OperationProductionStock>? OperationProductionStocks { get; set; }
    }
}
