namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class SiteCourseScheduleEntity
    {
        // // AcademicYear
        // public Guid AcademicYearId { get; set; }
        // public virtual AcademicYearEntity AcademicYearEntity { get; set; } = null!;

        // Site
        public Guid SiteId { get; set; }
        public virtual SiteEntity SiteEntity { get; set; } = null!;

        // CourseSchedule
        public Guid ScheduleId { get; set; }
        public virtual CourseScheduleEntity CourseScheduleEntity { get; set; } = null!;
    }
}
