using EmploymentServiceWebApp.Areas.Identity.Data;
using EmploymentServiceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmploymentServiceWebApp.Controllers
{
	public class EmployeeController : Controller
	{
		public ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;

		private List<Vacancy> _list;
		public EmployeeController(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			return View();
		}


		//public async Task<IActionResult> EditEmployee(string id)
		//{
		//	var employee = await _userManager.FindByIdAsync(id);
		//	UserPassword userPassword = new UserPassword()
		//	{
		//		User = employee,
		//		Password = ""
		//	};
		//	return View(userPassword);
		//}

		public async Task<IActionResult> ViewEmployeeDetail(string id)
		{
			var employee = await _userManager.FindByIdAsync(id);
			UserPassword userPassword = new UserPassword()
			{
				User = employee,
				Password = ""
			};
			return View(userPassword);
		}
		public async Task<IActionResult> DeleteEmployee(string id) 
		{
			var user = await _userManager.FindByIdAsync(id);
			var vacancyEmployee = await _context.VacancyEmployees.Where(ve => ve.EmployeeId == id).ToListAsync();

			_context.VacancyEmployees.RemoveRange(vacancyEmployee);
			await _userManager.DeleteAsync(user);
			await _context.SaveChangesAsync();
			//var vacancy = await _context.Vacancies.FindAsync(id);
			//var vacancyEmployee = await _context.VacancyEmployees.Where(ve => ve.VacancyId == id).ToListAsync();

			//_context.VacancyEmployees.RemoveRange(vacancyEmployee);
			//_context.Vacancies.Remove(vacancy);
			//await _context.SaveChangesAsync();
			return View();
		}


		public async Task<IActionResult> Edit(UserPassword model)
		{
			User user = new User();
			if (model.User.Email  != null)
			{
				user.Email = model.User.Email;
			}
			if (model.User.PhoneNumber != null)
			{
				user.PhoneNumber = model.User.PhoneNumber;
			}
			if(model.User.Name != null)
			{
				user.Name = model.User.Name;
			}
			if(model.User.SurName != null)
			{
				user.SurName = model.User.SurName;
			}
			if(model.User.MiddleName != null)
			{
				user.MiddleName = model.User.MiddleName;
			}

			




			if (model.Password != null)
			{

			}
			return View();
		}
	}
}
