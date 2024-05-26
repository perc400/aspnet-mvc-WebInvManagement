using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;
using WebInvManagement.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using iText.Kernel.Font;

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

            var font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");
            document.SetFont(font);

            // Добавляем заголовок
            document.Add(new Paragraph("Отчет о наименовании определенного запаса"));

            // Добавляем данные о запасе
            document.Add(new Paragraph($"Наименование товара: {stock.Title}"));
            document.Add(new Paragraph($"Группа, присвоенная после ABC-анализа: {abcGroup}"));
            document.Add(new Paragraph($"Склад (местонахождение): {warehouse.Title}"));

            // Добавляем таблицу с операциями
            var table = new Table(3);
            table.AddCell("Дата последней операции");
            table.AddCell("Количество операций, связанных с запасом");
            table.AddCell("Общая стоимость операций");
            foreach (var operation in operations)
            {
                table.AddCell(operation.Date.ToString());
                table.AddCell(operation.Quantity.ToString());
                table.AddCell(operation.Price.ToString());
            }
            document.Add(table);

            // Получаем путь к изображению графика
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", $"chart_{stockId}.png");

            if (System.IO.File.Exists(imagePath))
            {
                // Добавление изображения в PDF-документ
                var image = new iText.Layout.Element.Image(ImageDataFactory.Create(imagePath));
                document.Add(image);
            }
            else
            {
                document.Add(new Paragraph("График не найден."));
            }

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

            // Шаг 1: Рассчитываем общие стоимости для каждого товара
            var stockValues = new Dictionary<int, decimal>();

            foreach (var stock in stocks)
            {
                var totalValue = stock.Cost * stock.Quantity;
                stockValues.Add(stock.Id, (decimal)totalValue);
                Console.WriteLine($"Общая стоимость для товара {stock.Id} равна {totalValue}");
            }

            // Шаг 2: Ранжируем товары по общей стоимости в убывающей последовательности
            var sortedStockValues = stockValues.OrderByDescending(kv => kv.Value).ToList();

            // Шаг 3: Рассчитываем долю затрат для каждого товара
            var totalValueAllStocks = sortedStockValues.Sum(kv => kv.Value);
            var relativeStockValues = new Dictionary<int, decimal>();
            foreach (var kv in sortedStockValues)
            {
                var relativeValue = kv.Value / totalValueAllStocks * 100;
                relativeStockValues.Add(kv.Key, relativeValue);
                Console.WriteLine($"Доля затрат для товара {kv.Key} равна {relativeValue}");
            }

            // Шаг 4: Расчет нарастающих итогов
            var cumulativePercentage = 0m;
            var cumulativePercentages = new Dictionary<int, decimal>();
            var cumulativePercentageList = new List<decimal>();
            foreach (var kv in sortedStockValues)
            {
                cumulativePercentage += relativeStockValues[kv.Key];
                cumulativePercentages.Add(kv.Key, cumulativePercentage);
                cumulativePercentageList.Add(cumulativePercentage);
                Console.WriteLine($"Нарастающий итог для товара {kv.Key} равен {cumulativePercentage}");
            }

            // Определение групп ABC
            int nA = 0, nB = 0;
            foreach (var kv in cumulativePercentages)
            {
                string abcGroup = "";
                if (kv.Value <= 80)
                {
                    abcGroup = "A";
                    nA++;
                }
                else if (kv.Value <= 95)
                {
                    abcGroup = "B";
                    nB++;
                }
                else
                {
                    abcGroup = "C";
                }

                // Находим ID группы ABC в базе данных
                var abcGroupId = await _context.ABCGroups
                    .Where(g => g.Group == abcGroup)
                    .Select(g => g.Id)
                    .FirstOrDefaultAsync();

                // Создаем связь между товаром и группой ABC
                var abcProductionStock = new ABCProductionStock
                {
                    ProductionStockId = kv.Key,
                    ABCId = (int)abcGroupId
                };

                _context.ABCProductionStocks.Add(abcProductionStock);
            }

            await _context.SaveChangesAsync();

            // Получаем обновленные данные о товарах с их группами ABC
            var stocksWithABCGroups = await _context.ProductionStocks
                .Include(s => s.ABCProductionStocks)
                .ThenInclude(abc => abc.ABCGroup)
                .ToListAsync();

            // Сортируем товары по группе ABC: сначала A, затем B, затем C
            var sortedStocksWithABCGroups = stocksWithABCGroups
                .OrderBy(s => s.ABCProductionStocks.FirstOrDefault()?.ABCGroup.Group)
                .ToList();

            // Передаем данные для графика во ViewBag
            ViewBag.CumulativePercentages = cumulativePercentageList;
            ViewBag.StockLabels = sortedStockValues.Select(kv => kv.Key).ToList();
            ViewBag.NA = nA;  // передаем количество товаров группы A
            ViewBag.NB = nA + nB;  // передаем количество товаров групп A и B

            // Передаем результаты анализа на представление
            return View(sortedStocksWithABCGroups);
        }
    }
}
