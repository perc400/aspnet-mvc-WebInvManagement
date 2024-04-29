using Microsoft.AspNetCore.Mvc;

namespace WebInvManagement.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
