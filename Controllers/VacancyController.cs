using EmploymentServiceWebApp.Areas.Identity.Data;
using EmploymentServiceWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace EmploymentServiceWebApp.Controllers
{
    public class VacancyController : Controller
    {
        public ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        private List<Vacancy> _list;
        public VacancyController(ApplicationDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployerError()
        {
            return View();
        }


		public async Task<IActionResult> Create()
        {
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var user = await _userManager.GetUserAsync(User);
            if ((user.CompanyName == null || user.Description == null) && currentUserRole != "Admin")
            {
                return RedirectToAction("EmployerError");
            }
			VacancyAddress vacancyAddress = new VacancyAddress();
            return View(vacancyAddress);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VacancyAddress model)
        {
            if (ModelState.IsValid)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = await _userManager.GetUserAsync(HttpContext.User);


                var address = model.Address;
                address.VacancyId = model.Vacancy.Id;
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();

                var vacancy = model.Vacancy;
                vacancy.VacancyNumber = model.ToString();
                vacancy.EmployerId = userId;
                vacancy.AddressId = address.Id;
                //vacancy.Address = address;
                //vacancy.EmployerId = user.Id;
                await _context.Vacancies.AddAsync(vacancy);
                await _context.SaveChangesAsync();

                var addressV = _context.Addresses.Find(vacancy.AddressId);
                addressV.VacancyId = vacancy.Id;
                await _context.SaveChangesAsync();

                //var vac = new Vacancy();
                //Address address = new Address
                //{
                //    City = model.Address.City,
                //    District = "",
                //    Region = "",
                //    TerritorialCommunity = "",
                //    Street = "",
                //    Vacancy = vac,
                //    VacancyId = vac.Id
                //};

                //// додаємо адресу до бази даних
                //_context.Addresses.Add(address);
                //_context.SaveChanges();

                //Vacancy vacancy = new Vacancy
                //{
                //    Position = model.Vacancy.Position,
                //    Salary = model.Vacancy.Salary,
                //    AddressId = address.Id, // встановлюємо зв'язок з адресою
                //    Address = address
                //};

                //// додаємо вакансію до бази даних
                //_context.Vacancies.Add(vacancy);
                //_context.SaveChanges();
                TempData["Position"] = vacancy.Position;
                return RedirectToAction("Confirm","Vacancy");

            }
            return View(model);
        }
        public IActionResult Confirm()
        {
            var position = TempData["position"];
            return View(position);
        }
        public async Task<IActionResult> ViewVacancy()
        {
            var list = await _context.Vacancies.ToListAsync();
            return View(list);
        }
        
        [HttpGet("/ViewVacancy/{id}")]
        public IActionResult ViewVacancyDetail(int id)
        {
            Vacancy vacancy = _context.Vacancies.Find(id);
            VacancyAddress vacancyAddress = new VacancyAddress();
            vacancyAddress.Vacancy = vacancy;
            vacancyAddress.Address = _context.Addresses.Find(vacancy.AddressId);
            return View(vacancyAddress);
        }
        [HttpPost]
        public async Task<IActionResult> Filter(FilterModel filterModel)
        {
            List<Vacancy> list = new List<Vacancy>();
            if (filterModel.SalaryLow != null)
            {
                list = await _context.Vacancies.Where(v => v.Salary > filterModel.SalaryLow).ToListAsync();
            }
            if (filterModel.SalaryHigh != null)
            {
                list = list.Where(v => v.Salary < filterModel.SalaryHigh).ToList();
            }
            if (filterModel.Position != null)
            {
                list = list.Where(v => v.Position == filterModel.Position).ToList();
            }
            List<Address> addresses = new List<Address>();
            List<Vacancy> newestlist = new List<Vacancy>();
            if (filterModel.City != null)
            {
                addresses = await _context.Addresses.Where(a => a.City == filterModel.City).ToListAsync();

                List<Vacancy> newList = new List<Vacancy>();
                foreach (Address address in addresses)
                {
                    newList.Add(_context.Vacancies.Find(address.Id));
                }
                newestlist = list.Intersect(newList).ToList();
            }
            else
            {
                newestlist = list;
            }
                
                
            

            if (filterModel.Sort == "Зарплатою")
            {
                newestlist = newestlist.OrderBy(l=>l.Salary).ToList();
            }
            else if (filterModel.Sort == "Датою")
            {
                newestlist = newestlist.OrderBy(l => l.Date).ToList();
            }
            
            TempData["ListData"] = JsonConvert.SerializeObject(newestlist);
            return RedirectToAction("ViewVacancySort");
        }
        public IActionResult ViewVacancySort()
        {
            if (TempData.ContainsKey("ListData"))
            {
                _list = JsonConvert.DeserializeObject<List<Vacancy>>(TempData["ListData"] as string);
                // Очистити дані з TempData, оскільки вони вже були використані
                TempData.Remove("ListData");
            }
            return View(_list);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vacancyId)
        {
            var vacancy = await _context.Vacancies.FindAsync(vacancyId);
            var user = await _userManager.GetUserAsync(HttpContext.User);

            await _context.VacancyEmployees.AddAsync(new VacancyEmployee()
            {
                EmployeeId = user.Id,
                VacancyId = vacancyId
            });

            //user.Vacancies.Add(vacancy);
            //await _userManager.UpdateAsync(user);

            await _context.SaveChangesAsync();
            
            return View();
        }

        [HttpGet("/ViewVacancyResume/{id}")]
        public async Task<IActionResult> ViewVacancyDetailResume(int id)
        {
            var vac = await _context.Vacancies.FindAsync(id);
            var veL = await _context.VacancyEmployees.Where(ve => ve.VacancyId == id).ToListAsync();

            List<string> veIds = veL.Select(obj => obj.EmployeeId).ToList();
            var users = await _userManager.Users.Where(v => veIds.Contains(v.Id)).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            var vacancyEmployee = await _context.VacancyEmployees.Where(ve => ve.VacancyId == id).ToListAsync();

            _context.VacancyEmployees.RemoveRange(vacancyEmployee);

            var address = await _context.Addresses.FindAsync(vacancy.AddressId);

			_context.Addresses.Remove(address);
			_context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
            return View();
        }

        public async Task<IActionResult> EditVacancy(int id)
        {
            var vacancy = await _context.Vacancies.FindAsync(id);
            var address = await _context.Addresses.Where(a => a.VacancyId == id).ToListAsync();
            VacancyAddress vacancyAddress = new VacancyAddress()
            {
                Vacancy = vacancy,
                Address = address.First()
            };

            return View(vacancyAddress);
        }

        public async Task<IActionResult> Edit(VacancyAddress model)
        {
            var vacancy = model.Vacancy;
            var address = model.Address;
            address.VacancyId = vacancy.Id;

            int vacancyId = vacancy.Id;
            int addressId = address.Id;

            _context.Vacancies.Update(vacancy);
            _context.Addresses.Update(address);



            await _context.SaveChangesAsync();  
            return View();
        }
    }
    
}
//var model = new Vacancy();
//    model = vacancy;
//    model.VacancyNumber = vacancy.Id.ToString();
//    //model.Date = DateTime.Now;
//    //model.Profession = vacancy.Profession;
//    //model.Salary = vacancy.Salary;
//    //model.Address = vacancy.Address;
//    //model.Address.City = vacancy.Address.City;
//    //model.Address.VacancyId = model.Id;
//    //model.Address.Vacancy = model;

//    _context.Vacancies.Add(model);
//    _context.SaveChanges();