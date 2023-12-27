using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace HotelBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
			ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
			ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetInt32("UserRole");
            var rooms= _context.Rooms.Where(x=>x.room_status==1).ToList();
            return View(rooms);
        }

        public IActionResult Privacy()
		{
			ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
			ViewBag.UserName = HttpContext.Session.GetString("UserName");
			return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        { 
				return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
			
		}
    }
}
