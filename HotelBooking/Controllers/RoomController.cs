using HotelBooking.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDBContext _context;
        public RoomController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
			ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
			ViewBag.UserName = HttpContext.Session.GetString("UserName");
			return View();
        }
        public async Task<IActionResult> Detail(int Id)
        {
			ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
			ViewBag.UserName = HttpContext.Session.GetString("UserName");
			if (Id == null) return RedirectToAction("Index");
            var roomsById = _context.Rooms.Where(c => c.room_id == Id).FirstOrDefault();

            return View(roomsById);
        }
    }
}
