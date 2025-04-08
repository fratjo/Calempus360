namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class CourseEntity
    {
        public Guid CourseId { get; set; }
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
        public virtual List<OptionCourseEntity> OptionsCourses { get; set; } = new();
        
        // CourseEquipmentType
        public virtual List<CourseEquipmentTypeEntity> EquipmentTypes { get; set; } = new();

        // Session
        public virtual List<SessionEntity> Sessions { get; set; } = new();
    }
}
