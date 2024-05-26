using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;
using WebInvManagement.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using SkiaSharp;
using Newtonsoft.Json;

namespace WebInvManagement.Controllers
{
    public class ABCAnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ABCAnalysisController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Report()
        {
            var stocks = _context.ProductionStocks.ToList();
            return View(stocks);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(int stockId)
        {
            // Получаем данные о запасе
            var stock = await _context.ProductionStocks
                .Include(s => s.OperationProductionStocks)
                .ThenInclude(ops => ops.Operation)
                .Include(s => s.WarehouseProductionStocks)
                .ThenInclude(wp => wp.Warehouse)
                .FirstOrDefaultAsync(s => s.Id == stockId);

            if (stock == null)
            {
                return NotFound();
            }

            // Получаем результат ABC-анализа для этого запаса
            var abcGroup = await _context.ABCProductionStocks
                .Where(abc => abc.ProductionStockId == stockId)
                .Select(abc => abc.ABCGroup.Group)
                .FirstOrDefaultAsync();

            // Получаем данные о связанных с запасом операциях
            var operations = stock.OperationProductionStocks.Select(ops => ops.Operation);

            // Получаем данные о складе, в котором находится запас
            var warehouse = stock.WarehouseProductionStocks.FirstOrDefault()?.Warehouse;

            // Создаем новый PDF-документ
            var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports", "Report.pdf");
            var writer = new PdfWriter(pdfPath);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // Добавляем заголовок
            document.Add(new Paragraph("Stock Report"));

            // Добавляем данные о запасе
            document.Add(new Paragraph($"Stock Title: {stock.Title}"));
            document.Add(new Paragraph($"ABC Group: {abcGroup}"));
            document.Add(new Paragraph($"Warehouse: {warehouse.Title}"));

            // Добавляем таблицу с операциями
            var table = new Table(3);
            table.AddCell("Date");
            table.AddCell("Number of operations");
            table.AddCell("Cost");
            foreach (var operation in operations)
            {
                table.AddCell(operation.Date.ToString());
                table.AddCell(operation.Quantity.ToString());
                table.AddCell(operation.Price.ToString());
            }
            document.Add(table);

            // Закрываем документ
            document.Close();

            // Возвращаем результат в виде PDF
            return File("/reports/Report.pdf", "application/pdf", "Report.pdf");
        }

        public async Task<IActionResult> Index()
        {
            // Очищаем таблицу ABCProductionStocks перед загрузкой новых данных
            _context.ABCProductionStocks.RemoveRange(_context.ABCProductionStocks);
            await _context.SaveChangesAsync();

            // Получаем все товары и связанные с ними операции
            var stocks = await _context.ProductionStocks
                .Include(s => s.OperationProductionStocks)
                .ThenInclude(ops => ops.Operation)
                .ToListAsync();

            // Создаем список для хранения оборота для каждого товара
            var turnover = new Dictionary<int, int>();

            // Считаем оборот для каждого товара
            foreach (var stock in stocks)
            {
                var totalTurnover = stock.OperationProductionStocks.Sum(ops => ops.Operation?.Quantity ?? 0);
                turnover.Add(stock.Id, totalTurnover);
            }

            // Сортируем товары по обороту
            var sortedStocks = stocks.OrderByDescending(s => turnover[s.Id]);

            // Рассчитываем суммарный оборот для всех товаров
            var totalTurnoverAllStocks = turnover.Values.Sum();

            // Рассчитываем долю оборота для каждого товара
            var relativeTurnover = new Dictionary<int, double>();
            foreach (var stock in sortedStocks)
            {
                var relativeTurnoverValue = (double)turnover[stock.Id] / totalTurnoverAllStocks * 100;
                relativeTurnover.Add(stock.Id, relativeTurnoverValue);
            }

            // Сохраняем результаты анализа в базе данных
            foreach (var stock in sortedStocks)
            {
                // Находим соответствующую группу ABC для товара
                string abcGroup = "";
                if (relativeTurnover[stock.Id] >= 80)
                {
                    abcGroup = "A";
                }
                else if (relativeTurnover[stock.Id] >= 15)
                {
                    abcGroup = "B";
                }
                else
                {
                    abcGroup = "C";
                }

                // Находим ID группы ABC в базе данных
                int abcGroupId = 0;
                switch (abcGroup)
                {
                    case "A":
                        abcGroupId = 4; // ID группы A в базе данных
                        break;
                    case "B":
                        abcGroupId = 5; // ID группы B в базе данных
                        break;
                    case "C":
                        abcGroupId = 6; // ID группы C в базе данных
                        break;
                    default:
                        // Обработка ошибки
                        break;
                }

                // Создаем связь между товаром и группой ABC
                var abcProductionStock = new ABCProductionStock
                {
                    ProductionStockId = stock.Id,
                    ABCId = abcGroupId
                };

                _context.ABCProductionStocks.Add(abcProductionStock);
            }

            await _context.SaveChangesAsync();

            // Получаем обновленные данные о товарах с их группами ABC
            var stocksWithABCGroups = await _context.ProductionStocks
                .Include(s => s.ABCProductionStocks)
                .ThenInclude(abc => abc.ABCGroup)
                .ToListAsync();

            // Сортируем товары по группе ABC
            stocksWithABCGroups = stocksWithABCGroups.OrderByDescending(s => s.ABCProductionStocks.FirstOrDefault()?.ABCGroup?.Group).ToList();

            // Передаем результаты анализа на представление
            return View(stocksWithABCGroups);
        }
    }
}
