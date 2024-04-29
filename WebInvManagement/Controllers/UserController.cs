using Microsoft.AspNetCore.Mvc;

namespace WebInvManagement.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
