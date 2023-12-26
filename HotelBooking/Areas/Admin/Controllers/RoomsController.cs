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
using System.Security.AccessControl;

namespace HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class RoomsController : Controller
    {
        private readonly AppDBContext _context;

        public RoomsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("rooms")]
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Rooms.Include(r => r.category);
            return View(await appDBContext.ToListAsync());
        }

        [HttpGet("rooms/detail/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.category)
                .FirstOrDefaultAsync(m => m.room_id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpGet("rooms/create")]
        public IActionResult Create()
        {
            ViewData["category_name"] = new SelectList(_context.Categories, "category_name", "category_name");
            return View();
        }

        [HttpPost("rooms/create"), ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("room_name,category_id,room_desc,room_content,room_price,room_status")] Room room, IFormFile roomImage, string categoryName)
        {
            if (ModelState.IsValid)
            {
                string fileName = uploadedFile(room.room_name, roomImage);
                room.room_image = fileName;
                room.category_id = _context.getCategoryIdRoom(categoryName);
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }
        private string uploadedFile(string room_name, IFormFile roomImage)
        {
            string name = "";
            string fileName = $"{room_name}{Path.GetExtension(roomImage.FileName)}";
            name = fileName;
            var filePath = Path.Combine("wwwroot/public/uploads/room", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                roomImage.CopyTo(fileStream);
            }
            return name;
        }
        [HttpGet("rooms/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["category_name"] = new SelectList(_context.Categories, "category_name", "category_name");
            return View(room);
        }

        [HttpPost("rooms/edit/{id}"), ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("room_name,room_desc,room_content,room_price,room_status")] Room room, IFormFile? roomImage, string categoryName)
        {
            if (ModelState.IsValid)
            {
                var roomTmp = _context.Rooms.Find(id);
                try
                {
                    roomTmp.room_name = room.room_name;
                    roomTmp.category_id = _context.getCategoryIdRoom(categoryName);
                    roomTmp.room_desc = room.room_desc;
                    roomTmp.room_content = room.room_content;
                    roomTmp.room_price = room.room_price;
                    roomTmp.room_status = room.room_status;
                    if (roomImage != null)
                    {
                        FileInfo file = new FileInfo($"wwwroot/public/uploads/room/{roomTmp.room_image}");
                        file.Delete();
                        roomTmp.room_image = uploadedFile(roomTmp.room_name, roomImage);
                    }
                    _context.Update(roomTmp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.room_id))
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
            return View(room);
        }

        [HttpGet("rooms/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.category)
                .FirstOrDefaultAsync(m => m.room_id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost("rooms/delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'AppDBContext.Rooms'  is null.");
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                FileInfo file = new FileInfo($"wwwroot/public/uploads/room/{room.room_image}");
                file.Delete();
                _context.Rooms.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return (_context.Rooms?.Any(e => e.room_id == id)).GetValueOrDefault();
        }
    }
}