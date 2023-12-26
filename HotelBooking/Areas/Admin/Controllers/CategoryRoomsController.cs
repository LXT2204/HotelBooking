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
    public class CategoryRoomsController : Controller
    {
        private readonly AppDBContext _context;

        public CategoryRoomsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("categoryrooms")]
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Categories'  is null.");
        }

        [HttpGet("categoryrooms/detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryRoom = await _context.Categories
                .FirstOrDefaultAsync(m => m.category_id == id);
            if (categoryRoom == null)
            {
                return NotFound();
            }

            return View(categoryRoom);
        }

        [HttpGet("categoryrooms/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("categoryrooms/create")]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("category_name,category_desc,category_status")] CategoryRoom categoryRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryRoom);
        }

        [HttpGet("categoryrooms/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryRoom = await _context.Categories.FindAsync(id);
            if (categoryRoom == null)
            {
                return NotFound();
            }
            return View(categoryRoom);
        }

        [HttpPost("categoryrooms/edit/{id}")]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("category_id, category_name,category_desc,category_status")] CategoryRoom categoryRoom)
        {
            if (id != categoryRoom.category_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryRoomExists(categoryRoom.category_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryRoom);
        }

        [HttpGet("categoryrooms/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryRoom = await _context.Categories
                .FirstOrDefaultAsync(m => m.category_id == id);
            if (categoryRoom == null)
            {
                return NotFound();
            }

            return View(categoryRoom);
        }

        // POST: Admin/CategoryRooms/Delete/5
        [HttpPost("categoryrooms/delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'AppDBContext.Categories'  is null.");
            }
            var categoryRoom = await _context.Categories.FindAsync(id);
            if (categoryRoom != null)
            {
                _context.Categories.Remove(categoryRoom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryRoomExists(int id)
        {
          return (_context.Categories?.Any(e => e.category_id == id)).GetValueOrDefault();
        }
    }
}
