namespace EmploymentServiceWebApp.Models
{
	public class FilterModel
	{
		public string? City { get; set; }
		public string? Position { get; set; }
		public int SalaryLow { get; set; } = 0;
		public int SalaryHigh { get; set; } = int.MaxValue;
		public string? Sort { get; set; }
	}
}
