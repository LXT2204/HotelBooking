using HotelBooking.Data;
using HotelBooking.Models;
using HotelBooking.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDBContext _context;

        public UsersController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("users/create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet("users/login")]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost("users/create"), ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,email,password,phone,address")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.password = BCrypt.Net.BCrypt.HashPassword(users.password);
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(users);
        }
        [HttpPost("users/login"), ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userLogIn = _context.Users.FirstOrDefault(c => c.email == model.email);
            if (userLogIn == null)
            {
                return RedirectToAction(nameof(Login));
            }
            if (BCrypt.Net.BCrypt.Verify(model.password, userLogIn.password))
            {
                var data = _context.Users.Where(c => c.email == model.email).ToList();
				/*Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                Session["Email"] = data.FirstOrDefault().Email;
                Session["idUser"] = data.FirstOrDefault().idUser;*/
				HttpContext.Session.SetInt32("UserId", data.FirstOrDefault().id);
                HttpContext.Session.SetString("UserName", data.FirstOrDefault().name);
                HttpContext.Session.SetInt32("UserRole", data.FirstOrDefault().user_role);
                return RedirectToAction("Index", "Home");
            }
            else
            { 
                return RedirectToAction(nameof(Login));
            }
        }
        public IActionResult Logout() {
            HttpContext.Session.Clear();
			return RedirectToAction(nameof(Login));

		}

	}
}
