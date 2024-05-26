using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;
using WebInvManagement.Models;

namespace WebInvManagement.Controllers
{
    public class XYZAnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public XYZAnalysisController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            _context.XYZProductionStocks.RemoveRange(_context.XYZProductionStocks);
            await _context.SaveChangesAsync();

            var stocks = await _context.ProductionStocks
                .Include(ps => ps.OperationProductionStocks)
                .ThenInclude(ops => ops.Operation)
                .ToListAsync();

            foreach (var ps in stocks)
            {
                // Создаем список для хранения спроса для каждой операции
                List<int> demands = new List<int>();

                foreach (var ops in ps.OperationProductionStocks)
                {
                    var operation = ops.Operation;
                    if (operation != null && operation.Quantity.HasValue)
                    {
                        // Добавляем спрос для каждой операции в список demands
                        demands.Add(operation.Quantity.Value);
                    }
                }

                foreach (var item in demands)
                {
                    Console.WriteLine($"B_i_j = {item}");
                }

                if (demands.Count == 0) continue;

                // Расчет среднего значения спроса (B_i_сред)
                double avgDemand = demands.Average();
                Console.WriteLine($"B_i_сред = {avgDemand}");

                // Расчет стандартного отклонения спроса
                double sumSquaredDiff = demands.Select(d => Math.Pow(d - avgDemand, 2)).Sum();
                double stdDeviation = Math.Sqrt(sumSquaredDiff / demands.Count);

                // Расчет коэффициента вариации (K_i)
                double coeffVariation = stdDeviation / avgDemand;
                Console.WriteLine($"K_i_сред = {coeffVariation}");
            }

            await _context.SaveChangesAsync();

            // Получаем обновленные данные о товарах с их группами XYZ
            var stocksWithXYZGroups = await _context.ProductionStocks
                .Include(s => s.XYZProductionStocks)
                .ThenInclude(xyz => xyz.XYZGroup)
                .ToListAsync();

            // Сортируем товары по группе XYZ: сначала X, затем Y, затем Z
            var sortedStocksWithXYZGroups = stocksWithXYZGroups
                .OrderBy(s => s.XYZProductionStocks.FirstOrDefault()?.XYZGroup.Group)
                .ToList();

            // Передаем результаты анализа на представление
            return View(sortedStocksWithXYZGroups);
        }
    }
}
