using Calempus360.Infrastructure.Data.ModelConfiguration;
using Calempus360.Infrastructure.Persistence.Configurations;
using Calempus360.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Data;

public class Calempus360DbContext(DbContextOptions<Calempus360DbContext> options) : DbContext(options)
{
    public DbSet<UniversityEntity> Universities { get; set; }
    public DbSet<SiteEntity> Sites { get; set; }
    public DbSet<AcademicYearEntity> AcademicYears { get; set; }
    public DbSet<DayWithoutCourseEntity> DaysWithoutCourse { get; set; }
    public DbSet<StudentGroupEntity> StudentGroups { get; set; }
    public DbSet<OptionEntity> Options { get; set; }
    public DbSet<OptionCourseEntity> OptionCourse { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<EquipmentTypeEntity> EquipmentTypes { get; set; }
    public DbSet<EquipmentEntity> Equipments { get; set; }
    public DbSet<CourseEquipmentTypeEntity> CoursesEquipmentTypes { get; set; }
    public DbSet<UniversitySiteEquipmentEntity> UniversitiesSitesEquipments { get; set; }
    public DbSet<CourseScheduleEntity> CoursesSchedules { get; set; }
    public DbSet<SiteCourseScheduleEntity> SitesCoursesSchedules { get; set; }
    public DbSet<ClassroomEntity> Classrooms { get; set; }
    public DbSet<ClassroomEquipmentEntity> ClassroomsEquipments { get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<EquipmentSessionEntity> EquipmentSessions { get; set; }
    public DbSet<StudentGroupSessionEntity> StudentGroupSessions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UniversityConfiguration());
        modelBuilder.ApplyConfiguration(new SiteConfiguration());
        modelBuilder.ApplyConfiguration(new AcademicYearConfiguration());
        modelBuilder.ApplyConfiguration(new DayWithoutCourseConfiguration());
        modelBuilder.ApplyConfiguration(new StudentGroupConfiguration());
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
        modelBuilder.ApplyConfiguration(new SessionConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentSessionConfiguration());
        modelBuilder.ApplyConfiguration(new StudentGroupSessionConfiguration());

    }
}