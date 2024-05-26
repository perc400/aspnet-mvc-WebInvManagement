using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;
using WebInvManagement.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WebInvManagement.Controllers
{
    public class ProductionStockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionStockController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            List<ProductionStock> stocks = _context.ProductionStocks.ToList();

            return View(stocks);
        }

        [HttpPost]
        public IActionResult Calculate(int stockId, double turnover, double leadTime, double leadTimeDelay)
        {
            // Получаем объект ProductionStock из базы данных по его Id
            var stock = _context.ProductionStocks.Find(stockId);

            if (stock != null)
            {
                // Рассчитываем основные параметры движения запасов
                double dailyConsumption = turnover / 250; // Предполагаем 250 рабочих дней в году
                double safetyStock = dailyConsumption * leadTimeDelay;
                double expectedConsumptionDuringLeadTime = dailyConsumption * leadTime;
                double reorderPoint = safetyStock + expectedConsumptionDuringLeadTime;
                double optimalOrderSize = Math.Sqrt((double)((2 * stock.CarryingCostPerOrder * stock.AnnualDemand) / (stock.HoldingCostPerUnitPerYear * (leadTime + leadTimeDelay))));
                double maximumDesirableStockLevel = safetyStock + optimalOrderSize;

                // Обновляем поля объекта ProductionStock
                stock.DailyConsumption = (int)Math.Floor(dailyConsumption);
                stock.SafetyStock = (int)Math.Floor(safetyStock);
                stock.ExpectedConsumptionDuringLeadTime = (int)Math.Floor(expectedConsumptionDuringLeadTime);
                stock.ReorderPoint = (int)Math.Floor(reorderPoint);
                stock.OptimalOrderSize = (int)Math.Floor(optimalOrderSize);
                stock.MaximumDesirableStockLevel = (int)Math.Floor(maximumDesirableStockLevel);

                // Сохраняем изменения в базе данных
                _context.SaveChanges();

                // Возвращаем сообщение об успешном расчете
                return Ok("Расчет параметров движения запасов выполнен успешно.");
            }

            // Если объект ProductionStock не найден, возвращаем пользователю ошибку 404
            return NotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SimulateStockMovements(int stockId, DateTime startDate, DateTime endDate)
        {
            // Получаем объект ProductionStock из базы данных по stockId
            var stock = _context.ProductionStocks.FirstOrDefault(s => s.Id == stockId);

            if (stock == null || startDate >= endDate)
            {
                return BadRequest("Invalid stock or date range");
            }

            // Устанавливаем начальные значения для компонентов
            int currentStockLevel = stock.MaximumDesirableStockLevel ?? 0;
            DateTime orderPlacementTime = startDate; // Время размещения заказа
            DateTime orderCompletionTime = startDate.AddDays(stock.OptimalOrderSize ?? 0); // Время завершения заказа

            // Списки для хранения данных для графика
            var datesList = new List<string>();
            var quantitiesList = new List<int>();
            var reorderPointsList = new List<int>();
            var leadTimeList = new List<int>();
            var leadTimeDelayList = new List<int>();
            var expectedConsumptionList = new List<int>();

            // Проходим по каждому дню в указанном периоде
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Добавляем текущую дату в список дат
                datesList.Add(date.ToShortDateString());

                // Проверяем, достиг ли уровень запаса порогового уровня
                if (currentStockLevel <= stock.ThresholdLevel)
                {
                    // В этот день делаем заказ и пополняем запасы
                    currentStockLevel += stock.OptimalOrderSize ?? 0;
                    orderPlacementTime = date;
                    orderCompletionTime = date.AddDays(stock.OptimalOrderSize ?? 0); // Пересчитываем время завершения заказа
                }

                // Рассчитываем время выполнения заказа и время задержки поставки
                int leadTime = (int)Math.Ceiling((orderCompletionTime - date).TotalDays);
                int leadTimeDelay = (int)Math.Ceiling((date - orderPlacementTime).TotalDays);

                // Рассчитываем ожидаемое потребление за время выполнения заказа
                int expectedConsumptionDuringLeadTime = stock.ExpectedConsumptionDuringLeadTime ?? 0;

                // Добавляем текущие значения компонентов в соответствующие списки
                quantitiesList.Add(currentStockLevel);
                reorderPointsList.Add(stock.ThresholdLevel ?? 0);
                leadTimeList.Add(leadTime);
                leadTimeDelayList.Add(leadTimeDelay);
                expectedConsumptionList.Add(expectedConsumptionDuringLeadTime);

                // Уменьшаем количество товара на складе на величину ожидаемого потребления
                currentStockLevel -= expectedConsumptionDuringLeadTime;

                // Если уровень запаса стал меньше нуля, это означает, что был превышен гарантийный запас
                if (currentStockLevel < 0)
                {
                    // Корректируем уровень запаса до нуля
                    currentStockLevel = 0;
                }

                // Создаем запись о движении товара
                var stockMovement = new StockMovement
                {
                    Date = date,
                    Quantity = -expectedConsumptionDuringLeadTime // Отрицательное значение для отражения потребления товара
                };

                // Связываем запись StockMovement с соответствующим объектом ProductionStock
                var stockMovementProductionStock = new StockMovementProductionStock
                {
                    StockMovement = stockMovement,
                    ProductionStock = stock
                };

                // Добавляем записи в контекст базы данных
                _context.StockMovements.Add(stockMovement);
                _context.StockMovementProductionStocks.Add(stockMovementProductionStock);
            }

            // Сохраняем изменения в базе данных
            _context.SaveChanges();

            // Возвращаем данные в формате JSON
            return Json(new
            {
                dates = datesList,
                quantities = quantitiesList,
                reorderPoints = reorderPointsList,
                leadTime = leadTimeList,
                leadTimeDelay = leadTimeDelayList,
                expectedConsumption = expectedConsumptionList
            });
        }

    }
}
