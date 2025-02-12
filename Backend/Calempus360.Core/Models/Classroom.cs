namespace Calempus360.Core.Models
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // Site
        public int SiteId { get; set; }
        public virtual Site Site { get; set; } = null!;
        
        // ClassroomEquipment
        public List<ClassroomEquipment> ClassroomEquipments { get; set; }
        
        // ClassroomAcademicYear
        public List<ClassroomAcademicYear> ClassroomAcademicYears { get; set; } 
        
        // Session
        public List<Session> Sessions { get; set; }

    }
}
