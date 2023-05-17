namespace EmploymentServiceWebApp.Models
{
    public class VacancyAddress
    {
        public Vacancy Vacancy { get; set; } = new Vacancy();
        public Address Address { get; set; } = new Address();

    }
}
