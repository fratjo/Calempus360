namespace Calempus360.Core.Models
{
    public class Equipment
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
        public Guid EquipmentTypeId { get; set; }
        public virtual EquipmentType EquipmentType { get; set; } = null!;
        
        // UniversitySiteEquipment
        public virtual UniversitySiteEquipment UniversitySiteEquipment { get; set; } = null!;
        
        // ClassroomEquipment
        public virtual ClassroomEquipment ClassroomEquipment { get; set; }
        
        // EquipmentSession
        public virtual List<EquipmentSession> EquipmentSessions { get; set; }
    }
}
