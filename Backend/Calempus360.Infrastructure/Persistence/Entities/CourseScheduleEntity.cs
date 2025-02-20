namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class CourseScheduleEntity
    {
        public Guid ScheduleId { get; set; }
        public int DayOfTheWeek { get; set; }
        public TimeOnly HourStart { get; set; }
        public TimeOnly HourEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // SiteCourseSchedule
        public List<SiteCourseScheduleEntity> SitesCourseSchedules { get; set; }
    }
}
