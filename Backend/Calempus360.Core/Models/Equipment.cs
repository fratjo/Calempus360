namespace Calempus360.Core.Models
{
    public class Equipment
    {
        public int Equipment_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int EquipmentType_Id { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public UniversitySiteEquipment UniversitySiteEquipment { get; set; }
        public ClassroomEquipment Classroom { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
