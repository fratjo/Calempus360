namespace Calempus360.Core.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int TotalHours { get; set; }
        public int WeeklyHours { get; set; }
        public string Semester { get; set; }
        public int Credits { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // OptionCourse
        public virtual List<OptionCourse> OptionsCourses { get; set; } = new();
        
        // CourseEquipmentType
        public virtual List<CourseEquipmentType> EquipmentTypes { get; set; }
        
        //
        public virtual List<Session> Sessions { get; set; }
    }
}
