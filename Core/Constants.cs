namespace EmploymentServiceWebApp.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string Employee = "Employee";
            public const string Employer = "Employer";
        }

        public static class Policies 
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireEmployee = "RequireEmployee";
            public const string RequireEmployer = "RequireEmployer";
        }
            
    }
}
