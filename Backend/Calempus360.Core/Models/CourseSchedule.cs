namespace Calempus360.Models.Models
{
    public class CourseSchedule
    {
        public int Schedule_Id { get; set; }
        public int DayOfTheWeek { get; set; }
        public DateTime HourStart { get; set; }
        public DateTime HourEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<SiteCourseSchedule> Sites { get; set; }
    }
}
