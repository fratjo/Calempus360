namespace Calempus360.Models.Models
{
    public class Classroom
    {
        public int Classroom_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Site_Id { get; set; }
        public Site Site { get; set; }
        public List<ClassroomEquipment> Equipments { get; set; }
        public List<ClassroomAcademicYear> AcademicYears { get; set; }
        public List<Session> Sessions { get; set; }
        
    }
}
