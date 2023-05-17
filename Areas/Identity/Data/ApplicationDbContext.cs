using EmploymentServiceWebApp.Models;
using EmploymentServiceWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploymentServiceWebApp.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        /*Many to Many*/
        /*Employee - Vacancy*/


        //builder.Entity<Employee>()
        //    .HasMany(e => e.Vacancies)
        //    .WithMany(e => e.Employees);

        ///*One to One*/
        ///*Vacancy - Address*/

        //builder.Entity<Address>()
        //    .HasOne(e => e.Vacancy)
        //    .WithOne(e => e.Address)
        //    .HasForeignKey<Address>(e => e.VacancyId)

        // /*.IsRequired()*/;

        ///*One to Many*/
        ///*Employer - Vacancy*/

        //builder.Entity<Employer>()
        //    .HasMany(e => e.Vacancies)
        //    .WithOne(e => e.Employer)
        //    .HasForeignKey(e => e.EmployerId)

            /*.IsRequired()*/;
        
        base.OnModelCreating(builder);


        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //builder.ApplyConfiguration(new UserEntityConfiguration());
    }
    //public DbSet<User> Users { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<VacancyEmployee> VacancyEmployees { get; set; }
    //public DbSet<Admin> Admins { get; set; }
}

//internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
//{
//    public void Configure(EntityTypeBuilder<User> builder)
//    {
//        builder.Property(u => u.Email).HasMaxLength(64);
//        builder.Property(u => u.PhoneNumber).HasMaxLength(13);
//        throw new NotImplementedException();
//    }
//}