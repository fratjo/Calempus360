using Calempus360.Infrastructure.Data.ModelConfiguration;
using Calempus360.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<University> Universities { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<AcademicYear> AcademicYears { get; set; }
    public DbSet<DayWithoutCourse> DaysWithoutCourse { get; set; }
    public DbSet<SiteAcademicYear> SitesAcademicYear { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<OptionCourse> OptionCourse { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<CourseEquipmentType> CoursesEquipmentTypes { get; set; }
    public DbSet<UniversitySiteEquipment> UniversitiesSitesEquipments { get; set; }
    public DbSet<CourseSchedule> CoursesSchedules { get; set; }
    public DbSet<SiteCourseSchedule> SitesCoursesSchedules { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<ClassroomEquipment> ClassroomsEquipments { get; set; }
    public DbSet<ClassroomAcademicYear> ClassroomsAcademicYear { get; set; }
    public DbSet<Session> Sessions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UniversityConfiguration());
        modelBuilder.ApplyConfiguration(new SiteConfiguration());
        modelBuilder.ApplyConfiguration(new AcademicYearConfiguration());
        modelBuilder.ApplyConfiguration(new DayWithoutCourseConfiguration());
        modelBuilder.ApplyConfiguration(new SiteAcademicYearConfiguration());
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new OptionCourseConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CourseEquipmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new UniversitySiteEquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new CourseScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new SiteCourseScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new ClassroomConfiguration());
        modelBuilder.ApplyConfiguration(new ClassroomEquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new ClassroomAcademicYearConfiguration());
        modelBuilder.ApplyConfiguration(new SessionConfiguration());

    }
}