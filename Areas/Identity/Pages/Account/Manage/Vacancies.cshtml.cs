using EmploymentServiceWebApp.Areas.Identity.Data;
using EmploymentServiceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmploymentServiceWebApp.Areas.Identity.Pages.Account.Manage
{
    public class VacanciesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public ApplicationDbContext _context;

        public VacanciesModel(ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public string UserId { get; set; } = string.Empty;
        public List<Vacancy> Vacancies { get; set; } = new List<Vacancy>();

        public async Task GetIdAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            UserId = user.Id;
        }

        public async Task<List<Vacancy>> GetListVacancies()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Vacancies = await _context.Vacancies.Where(u => u.EmployerId == user.Id).ToListAsync();
            return Vacancies;
        }
        public async Task<List<Vacancy>> GetListVacanciesEmployee()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var vacIds = await _context.VacancyEmployees.Where(ve => ve.EmployeeId == user.Id).ToListAsync();

            List<int> vacancyIds = vacIds.Select(obj => obj.VacancyId).ToList();

            Vacancies = await _context.Vacancies.Where(v => vacancyIds.Contains(v.Id)).ToListAsync();
            return Vacancies;
        }

        public async Task<List<Vacancy>> GetWholeListVacancies()
        {
            Vacancies = await _context.Vacancies.ToListAsync();
            return Vacancies;
        }
        public void OnGet()
        {
        }
    }
}
