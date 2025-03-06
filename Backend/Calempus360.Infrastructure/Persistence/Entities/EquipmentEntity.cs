namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class EquipmentEntity
    {
        public Guid EquipmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // EquipmentType
        public         Guid                 EquipmentTypeId     { get; set; }
        public virtual EquipmentTypeEntity? EquipmentTypeEntity { get; set; } = null!;
        
        // UniversitySiteEquipment
        public virtual UniversitySiteEquipmentEntity UniversitySiteEquipmentEntity { get; set; } = null!;
        
        // ClassroomEquipment
        public List<ClassroomEquipmentEntity>? ClassroomEquipments { get; set; }
        
        // EquipmentSession
        public virtual List<EquipmentSessionEntity>? EquipmentSessions { get; set; }
    }
}
