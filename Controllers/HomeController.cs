using EmploymentServiceWebApp.Areas.Identity.Data;
using EmploymentServiceWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmploymentServiceWebApp.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            /*(*/
            
            return View();
        }
		public IActionResult Check()
		{
			/*(*/

			return View();
		}
		//public async Task<IActionResult> LogOut()
		//{
		//    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		//    return RedirectToAction("Login","Access");
		//}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}