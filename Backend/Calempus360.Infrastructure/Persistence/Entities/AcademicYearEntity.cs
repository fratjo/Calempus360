namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class AcademicYearEntity
    {
        public string AcademicYearId { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // SiteAcademicYear
        public virtual List<SiteAcademicYearEntity> SiteAcademicYears { get; set; }
        
        // SiteCourseSchedule
        public virtual List<SiteCourseScheduleEntity> SiteCourseSchedules { get; set; }
        
        // ClassroomAcademicYear
        public virtual List<ClassroomAcademicYearEntity> ClassroomAcademicYears { get; set; }
        
        // StudentGroup
        public virtual List<StudentGroupEntity> StudentGroups { get; set; }
        
        // ClassroomEquipment
        public virtual List<ClassroomEquipmentEntity> ClassroomEquipments { get; set; }
        
        // UniversitySiteEquipment
        public virtual List<UniversitySiteEquipmentEntity> UniversitySiteEquipments { get; set; }
        
        // CourseEquipmentType
        public virtual List<CourseEquipmentTypeEntity> CourseEquipmentTypes { get; set; }
        
        // OptionCourse
        public virtual List<OptionCourseEntity> OptionCourses { get; set; }
        
        // DayWithoutCourse
        public virtual List<DayWithoutCourseEntity> DaysWithoutCourse { get; set; } = new();
    }
}
