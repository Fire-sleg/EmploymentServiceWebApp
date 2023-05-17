using EmploymentServiceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentServiceWebApp.Areas.Identity.Data
{
    public class DataSeeder
    {
        //private readonly ApplicationDbContext _context;

        //public DataSeeder(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                if (!context.Vacancies.Any())
                {
                    var vacancies = new List<Vacancy>()
                    {
                        new Vacancy()
                        {
                            Position = "1",
                            Salary = 1
                        }
                    };
                    context.Vacancies.AddRange(vacancies);
                    context.SaveChanges();
                }
            }


        }

    }
}
