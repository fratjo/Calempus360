namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class ClassroomEquipmentEntity
    {
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYearEntity AcademicYearEntity { get; set; } = null!;
        
        // Equipment
        public Guid EquipmentId { get; set; }
        public virtual EquipmentEntity EquipmentEntity { get; set; } = null!;
        
        // Classroom
        public Guid ClassroomId { get; set; }
        public virtual ClassroomEntity ClassroomEntity { get; set; } = null!;
    }
}
