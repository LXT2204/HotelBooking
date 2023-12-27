using Microsoft.AspNetCore.Mvc;
using HotelBooking.Models;
using HotelBooking.Data;
using HotelBooking.Repository;
using HotelBooking.Models.ViewModel;
namespace HotelBooking.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDBContext _context;
        public CartController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
             List<CartItemModel> cartItems=HttpContext.Session.GetJson<List<CartItemModel>>("Cart")?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
                }
    }
}
