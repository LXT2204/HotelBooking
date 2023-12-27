using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _context;
        public CategoryController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int Id)
        {

            if (Id == null) return RedirectToAction("Index");
            var roomsByCategory = _context.Rooms.Where(c => c.category_id == Id).Where(x=>x.room_status==1);

            return View(await roomsByCategory.OrderByDescending(c => c.category_id).ToListAsync()) ;
        }
    }
}
