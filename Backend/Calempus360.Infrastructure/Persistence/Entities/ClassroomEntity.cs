namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class ClassroomEntity
    {
        public Guid ClassroomId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // Site
        public Guid SiteId { get; set; }
        public virtual SiteEntity SiteEntity { get; set; } = null!;
        
        // ClassroomEquipment
        public List<ClassroomEquipmentEntity> ClassroomEquipments { get; set; }
        
        // ClassroomAcademicYear
        public List<ClassroomAcademicYearEntity> ClassroomAcademicYears { get; set; } 
        
        // Session
        public List<SessionEntity> Sessions { get; set; }

    }
}
