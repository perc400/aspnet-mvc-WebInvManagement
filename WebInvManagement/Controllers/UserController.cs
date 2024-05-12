﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebInvManagement.Data;
using WebInvManagement.Models;

namespace WebInvManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users
                .Include(u => u.Role)
                .ToList();

            return View(users);
        }
    }
}
