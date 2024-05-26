using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;
using WebInvManagement.Models;
using WebInvManagement.ViewModels;

namespace WebInvManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userInfoViewModels = new List<UserInfoViewModel>();

            foreach (var user in users)
            {
                var userInfoViewModel = new UserInfoViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };

                userInfoViewModels.Add(userInfoViewModel);
            }

            return View(userInfoViewModels);
        }
    }
}
