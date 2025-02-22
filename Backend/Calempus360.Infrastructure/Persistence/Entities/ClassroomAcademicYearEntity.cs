namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class ClassroomAcademicYearEntity
    {
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYearEntity AcademicYearEntity { get; set; } = null!;
        
        // Classroom
        public Guid ClassroomId { get; set; }
        public virtual ClassroomEntity ClassroomEntity { get; set; } = null!;
    }
}
