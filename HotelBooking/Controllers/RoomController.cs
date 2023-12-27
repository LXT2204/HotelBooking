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
            return View();
        }
        public async Task<IActionResult> Detail(int Id)
        {
            if (Id == null) return RedirectToAction("Index");
            var roomsById = _context.Rooms.Where(c => c.room_id == Id).FirstOrDefault();

            return View(roomsById);
        }
    }
}
