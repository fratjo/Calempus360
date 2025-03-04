namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class UniversitySiteEquipmentEntity
    {
        // AcademicYear
        public Guid AcademicYearId { get; set; }
        public AcademicYearEntity AcademicYearEntity { get; set; } = null!;
        
        // Equipment
        public Guid EquipmentId { get; set; }
        public EquipmentEntity EquipmentEntity { get; set; } = null!;
        
        // Site
        public Guid SiteId { get; set; }
        public SiteEntity SiteEntity { get; set; }
        
        // University
        public Guid UniversityId { get; set; }
        public UniversityEntity UniversityEntity { get; set; } = null!;
    }
}
