namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class AcademicYearEntity
    {
        public Guid AcademicYearId { get; set; }
        public string AcademicYearCode { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties

        // SiteCourseSchedule
        // public virtual List<SiteCourseScheduleEntity> SiteCourseSchedules { get; set; }

        // StudentGroup
        public virtual List<StudentGroupEntity> StudentGroups { get; set; }

        // ClassroomEquipment
        public virtual List<ClassroomEquipmentEntity> ClassroomEquipments { get; set; }

        // CourseEquipmentType
        public virtual List<CourseEquipmentTypeEntity> CourseEquipmentTypes { get; set; }

        // OptionCourse
        public virtual List<OptionCourseEntity> OptionCourses { get; set; }

        // DayWithoutCourse
        public virtual List<DayWithoutCourseEntity> DaysWithoutCourses { get; set; } = new();
    }
}
