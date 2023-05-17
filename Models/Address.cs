using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmploymentServiceWebApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("VacancyId")]
        public int VacancyId { get; set; } = 0;
        //public Vacancy? Vacancy { get; set; }
        public string? Region { get; set; } = string.Empty;
        public string? District { get; set; } = string.Empty;
        public string? TerritorialCommunity { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Street { get; set; } = string.Empty;
        public int BuildingNumber { get; set; } = 0;
    }
}
