using Calempus360.Infrastructure.Data.ModelConfiguration;
using Calempus360.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<University> Universities { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Academic_Year> Academic_Years { get; set; }
    public DbSet<DayWithoutCourse> DaysWithoutCourse { get; set; }
    public DbSet<Site_Academic_Year> Sites_Academic_Year { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<OptionCourse> OptionCourse { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UniversityConfiguration());
        modelBuilder.ApplyConfiguration(new SiteConfiguration());
        modelBuilder.ApplyConfiguration(new AcademicYearConfiguration());
        modelBuilder.ApplyConfiguration(new DayWithoutCourseConfiguration());
        modelBuilder.ApplyConfiguration(new Site_Academic_YearConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new OptionCourseConfiguration());

    }
}