namespace Calempus360.Core.Models
{
    public class University
    {
        public Guid UniversityId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // Site
        public virtual List<Site> Sites { get; set; } = new();
        
        // UniversitySiteEquipment
        public virtual List<UniversitySiteEquipment> Equipments { get; set; }
        
        // CourseEquipmentType
        public virtual List<CourseEquipmentType> CourseEquipmentTypes { get; set; }
    }
}
