namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class DayWithoutCourseEntity
    {
        public Guid DayWithoutCourseId { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYearEntity AcademicYearEntity { get; set; } = null!;
    }
}
