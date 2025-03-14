namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class EquipmentEntity
    {
        public Guid EquipmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties

        // EquipmentType
        public Guid EquipmentTypeId { get; set; }
        public virtual EquipmentTypeEntity? EquipmentTypeEntity { get; set; } = null!;

        // UniversitySiteEquipment
        public virtual UniversitySiteEquipmentEntity UniversitySiteEquipmentEntity { get; set; } = null!;

        // ClassroomEquipment
        public List<ClassroomEquipmentEntity>? ClassroomEquipments { get; set; } = null!;

        // EquipmentSession
        public virtual List<EquipmentSessionEntity>? EquipmentSessions { get; set; } = null!;
    }
}
