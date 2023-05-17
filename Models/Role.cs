using System.ComponentModel.DataAnnotations;

namespace EmploymentServiceWebApp.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;

    }
}
