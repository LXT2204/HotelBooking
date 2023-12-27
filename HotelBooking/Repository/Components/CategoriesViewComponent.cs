using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly AppDBContext _context;
        public CategoriesViewComponent(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categories.Where(x=>x.category_status==1).ToListAsync());
    }
}
