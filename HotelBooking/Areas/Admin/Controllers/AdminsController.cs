using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;
using BCrypt.Net;

namespace HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class AdminsController : Controller
    {
        private readonly AppDBContext _context;

        public AdminsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admins.ToListAsync());
        }
        [HttpPost("login"), ActionName("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Admins admin)
        {
            var isValid = _context.Admins.FirstOrDefault(a => a.admin_email == admin.admin_email);
            if (isValid != null)
            {
                if (BCrypt.Net.BCrypt.Verify(admin.admin_password, isValid.admin_password))
                {
                    return RedirectToAction("Showdashboard", "Admins");
                }
            }
            return View();
        }
        [HttpGet("dashboard"), ActionName("Showdashboard")]
        public async Task<IActionResult> Showdashboard()
        {
            return View();
        }
        [HttpGet("create_admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost("create_admin"), ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("admin_email,admin_password,admin_name,admin_phone")] Admins admins)
        {
            if (ModelState.IsValid)
            {
                admins.admin_password = BCrypt.Net.BCrypt.HashPassword(admins.admin_password);
                _context.Add(admins);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }
    }
}
