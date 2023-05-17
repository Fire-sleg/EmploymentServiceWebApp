using Microsoft.AspNetCore.Mvc;

namespace EmploymentServiceWebApp.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
