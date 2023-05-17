using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmploymentServiceWebApp.Models
{
    public class VacancyEmployee
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("VacancyId")]
        public int VacancyId { get; set; }
        [ForeignKey("EmployeeId")]
        public string? EmployeeId { get; set; }
    }
}
