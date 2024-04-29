using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;

namespace WebInvManagement.Controllers
{
    public class ProductionStockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
