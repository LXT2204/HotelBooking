using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.AspNetCore.Authorization;


namespace HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class OrderController : Controller
    {
        private readonly AppDBContext _context;
        public OrderController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet("orders")]
        public async Task<IActionResult> Index()
        {
            return _context.Orders != null ?
                        View(await _context.Orders.ToListAsync()) :
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
            return RedirectToAction(nameof(Index));
        }
        [HttpPost("order/confirm/{id}"), ActionName("Confirm")]
        public async Task<IActionResult> Confirm(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppDBContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.order_status = 1;
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("order/detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order_detail = await _context.OrderDetails.FindAsync(id);
            if (order_detail == null)
            {
                return NotFound();
            }

            return View(order_detail);
        }
    }
}
