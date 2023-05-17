using EmploymentServiceWebApp.Areas.Identity.Data;
using EmploymentServiceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EmploymentServiceWebApp.Controllers
{
    public class ResumeController : Controller
    {
        public ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public ResumeController(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile pdfFile)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    pdfFile.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.ResumePdf = fileBytes;
                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("/Identity/Account/Manage/Resume");
            }

            // Обробка випадку, коли файл не був завантажений
            // ...

            return View();
        }
    }
}
