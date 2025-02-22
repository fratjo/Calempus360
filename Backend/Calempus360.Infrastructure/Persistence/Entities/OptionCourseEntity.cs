namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class OptionCourseEntity
    {   
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYearEntity AcademicYearEntity { get; set; } = null!;
        
        // Course
        public Guid CourseId { get; set; }
        public virtual CourseEntity CourseEntity { get; set; } = null!;
        
        // Option
        public Guid OptionId { get; set; }
        public virtual OptionEntity OptionEntity { get; set; } = null!;
        
        // OptionGrade
        public int OptionGrade { get; set; }
    }
}
