using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace EmploymentServiceWebApp.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        public string? VacancyNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;


        //[Required]
        public string Position { get; set; } = string.Empty;

        //[Required] [Required(ErrorMessage ="Не указан возраст")]
        public uint Salary { get; set; } = 0;



        
        public string? Duties { get; set; } = string.Empty;
        public string? ModeOfOperation { get; set; } = string.Empty;
        public string? NatureOfWorkPerformed { get; set; } = string.Empty;
        public string? Additionally { get; set; } = string.Empty;
        public string? Profession { get; set; } = string.Empty;
        public string? EducationLevel { get; set; } = string.Empty;
        public uint WorkExperience { get; set; } = 0;

        //Relationships
        [ForeignKey("EmployerId")]
        public string? EmployerId { get; set; } = string.Empty;
        //public Employer? Employer { get; set; }

        [ForeignKey("AddressId")]
        public int AddressId { get; set; } = 0;
        [ForeignKey("VacancyEmployeeId")]
        public int VacancyEmployeeId { get; set; }


        //public Address? Address { get; set; }

        //public List<Employee>? Employees { get; set; }

        //public List<EmployeeVacancy> EmployeeVacancies { get; set; }

    }
}
