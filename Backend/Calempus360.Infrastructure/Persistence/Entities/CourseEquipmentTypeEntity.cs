namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class CourseEquipmentTypeEntity
    {
        // AcademicYear
        public Guid AcademicYearId { get; set; }
        public virtual AcademicYearEntity AcademicYearEntity { get; set; } = null!;
        
        // Course
        public Guid CourseId { get; set; }
        public virtual CourseEntity CourseEntity { get; set; } = null!;
        
        // EquipmentType
        public Guid EquipmentTypeId { get; set; }
        public virtual EquipmentTypeEntity EquipmentTypeEntity { get; set; } = null!;
        
        // University
        public Guid UniversityId { get; set; }
        public virtual UniversityEntity UniversityEntity { get; set; } = null!;
        
        // Quantity
        public int Quantity { get; set; } = 1;
        

    }
}
