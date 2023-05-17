using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EmploymentServiceWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace EmploymentServiceWebApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? MiddleName { get; set; }
    public string? SurName { get; set; }
    public string? CompanyName { get; set; }
    public string? Description { get; set; }
    public byte[]? ResumePdf { get; set; }
    //Relationships
    public List<Vacancy>? Vacancies { get; set; }
    [ForeignKey("VacancyEmployeeId")]
    public int VacancyEmployeeId { get; set; }

}


