using EmploymentServiceWebApp.Areas.Identity.Data;

namespace EmploymentServiceWebApp.Models
{
	public class UserPassword
	{
		public User User { get; set; }
		public string Password { get; set; }
	}
}
