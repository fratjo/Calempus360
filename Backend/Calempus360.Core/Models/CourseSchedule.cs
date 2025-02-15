namespace Calempus360.Core.Models
{
    public class CourseSchedule
    {
        public Guid ScheduleId { get; set; }
        public int DayOfTheWeek { get; set; }
        public TimeOnly HourStart { get; set; }
        public TimeOnly HourEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // SiteCourseSchedule
        public List<SiteCourseSchedule> SitesCourseSchedules { get; set; }
    }
}
