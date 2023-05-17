using EmploymentServiceWebApp.Areas.Identity.Data;
using EmploymentServiceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EmploymentServiceWebApp.Controllers
{
	public class EmployerController : Controller
	{
		public ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		private List<Vacancy> _list;
		public EmployerController(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> DeleteEmployer(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			var vacancies = await _context.Vacancies.Where(v => v.EmployerId == id).ToListAsync();
			List<int> vIds = vacancies.Select(obj => obj.Id).ToList();
			var vacEmpl = await _context.VacancyEmployees.Where(v => vIds.Contains(v.VacancyId)).ToListAsync(); // to remove

			List<int> aIds = vacancies.Select(obj => obj.AddressId).ToList();
			var addresses = await _context.Addresses.Where(a => aIds.Contains(a.Id)).ToListAsync(); // to remove

			_context.Addresses.RemoveRange(addresses);
			_context.Vacancies.RemoveRange(vacancies);
			await _userManager.DeleteAsync(user);

			await _context.SaveChangesAsync();
			return View();
		}
	}
}
