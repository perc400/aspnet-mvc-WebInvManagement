using Microsoft.AspNetCore.Mvc;
using WebInvManagement.Data;
using WebInvManagement.Models;

namespace WebInvManagement.Controllers
{
    public class ABCXYZAnalysisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ABCXYZAnalysisController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            List<ProductionStock> stocks = _context.ProductionStocks.ToList();

            return View(stocks);
        }
    }
}
