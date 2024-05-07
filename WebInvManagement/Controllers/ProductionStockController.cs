using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;
using WebInvManagement.Models;

namespace WebInvManagement.Controllers
{
    public class ProductionStockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionStockController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // Методы для работы с константными рабочими днями
        private double GetLeadTimeInWorkingDays()
        {
            // Здесь можно вернуть константное значение для времени выполнения заказа в рабочих днях
            return 22;
        }

        private double GetReorderCycleInWorkingDays()
        {
            // Здесь можно вернуть константное значение для периода перезаказа в рабочих днях
            return 30;
        }

        // Методы для расчета параметров системы управления запасами
        private double CalculateOptimalOrderSize(ProductionStock productionStock, double carryingCostPerOrder, double holdingCostPerUnitPerYear, double annualDemand)
        {
            // Рассчитываем оптимальный размер заказа по формуле Уилсона
            double leadTimeInDays = GetLeadTimeInWorkingDays();
            double reorderCycleInDays = GetReorderCycleInWorkingDays();
            double optimalOrderSize = Math.Sqrt((2 * carryingCostPerOrder * annualDemand) / (holdingCostPerUnitPerYear * (leadTimeInDays + reorderCycleInDays)));
            return optimalOrderSize;
        }

        private double CalculateSafetyStock(double dailyConsumption)
        {
            // Рассчитываем запас безопасности
            double leadTimeInWorkingDays = GetLeadTimeInWorkingDays();
            double safetyStock = dailyConsumption * leadTimeInWorkingDays;
            return safetyStock;
        }

        private double CalculateExpectedConsumptionDuringLeadTime(double dailyConsumption)
        {
            // Рассчитываем ожидаемое потребление за время выполнения заказа
            double leadTimeInDays = GetLeadTimeInWorkingDays();
            double expectedConsumptionDuringLeadTime = dailyConsumption * leadTimeInDays;
            return expectedConsumptionDuringLeadTime;
        }

        private double CalculateReorderPoint(double safetyStock, double expectedConsumptionDuringLeadTime)
        {
            // Рассчитываем точку перезаказа
            double reorderPoint = safetyStock + expectedConsumptionDuringLeadTime;
            return reorderPoint;
        }

        private double CalculateMaximumDesirableStockLevel(double optimalOrderSize, double safetyStock)
        {
            // Рассчитываем максимальный желательный уровень запасов
            double maximumDesirableStockLevel = optimalOrderSize + safetyStock;
            return maximumDesirableStockLevel;
        }

        public IActionResult Index()
        {
            // Получаем список объектов ProductionStock из базы данных
            List<ProductionStock> stocks = _context.ProductionStocks.ToList();

            // Примеры ваших данных для расчета параметров
            double carryingCostPerOrder = 100;
            double holdingCostPerUnitPerYear = 10;
            double annualDemand = 1000;
            double dailyConsumption = 5;

            // Проходимся по каждому объекту ProductionStock и расчитываем параметры для него
            foreach (var stock in stocks)
            {
                // Расчет параметров для текущего объекта ProductionStock
                double optimalOrderSize = CalculateOptimalOrderSize(stock, carryingCostPerOrder, holdingCostPerUnitPerYear, annualDemand);
                double safetyStock = CalculateSafetyStock(dailyConsumption);
                double expectedConsumptionDuringLeadTime = CalculateExpectedConsumptionDuringLeadTime(dailyConsumption);
                double reorderPoint = CalculateReorderPoint(safetyStock, expectedConsumptionDuringLeadTime);
                double maximumDesirableStockLevel = CalculateMaximumDesirableStockLevel(optimalOrderSize, safetyStock);

                // Присваиваем расчитанные параметры текущему объекту ProductionStock
                stock.OptimalOrderSize = Convert.ToInt32(optimalOrderSize);
                stock.SafetyStock = Convert.ToInt32(safetyStock);
                stock.ExpectedConsumptionDuringLeadTime = Convert.ToInt32(expectedConsumptionDuringLeadTime);
                stock.ReorderPoint = Convert.ToInt32(reorderPoint);
                stock.MaximumDesirableStockLevel = Convert.ToInt32(maximumDesirableStockLevel);
            }

            // Возвращаем представление Index, передавая ему список ProductionStock
            return View(stocks);
        }
    }
}
