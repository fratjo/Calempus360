namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class EquipmentTypeEntity
    {
        public Guid EquipmentTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // Equipment
        public virtual List<EquipmentEntity> Equipments { get; set; }
        
        // CourseEquipmentType
        public virtual List<CourseEquipmentTypeEntity> CourseEquipmentTypes { get; set; }
    }
}
