using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace HotelBooking.Controllers
{
	public class OrderController : Controller
	{
		private readonly AppDBContext _context;
		public OrderController(AppDBContext context)
		{
			_context = context;
		}
        [HttpPost("order"), ActionName("Order")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order([Bind("room_id, room_name, room_price, room_image, checkin, checkout")] OrderDetails model)
        {
            
                if (ModelState.IsValid)
                {
                    Random d = new Random();
                    int oId = d.Next(1000, 9999);
                    Order o = new Order
                    {
                        order_id = oId,
                        customer_id = (int)HttpContext.Session.GetInt32("UserId")
                    };
                    _context.Add(o);
                    model.order_id = oId;
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("");
                }
      
        }
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Role = HttpContext.Session.GetInt32("UserRole");
            return _context.Orders != null ?
                        View(await _context.Orders.Where(x=>x.customer_id == Id).ToListAsync()) :
                        Problem("Entity set 'AppDBContext.Categories'  is null.");
        }
        [HttpPost("order/delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppDBContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.order_status = 2;
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");


        }
    }
}
