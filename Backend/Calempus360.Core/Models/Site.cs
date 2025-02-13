using Calempus360.Core.Models;

namespace Calempus360.Core.Models
{
    public class Site
    {
        public Guid SiteId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // University
        public virtual Guid UniversityId { get; set; }
        public virtual University University { get; set; } = null!;
        
        // SiteAcademicYear
        public virtual string AcademicYearId { get; set; }
        public virtual List<SiteAcademicYear> SiteAcademicYears { get; set; } 
        
        // StudentGroup
        public virtual List<StudentGroup> StudentGroups { get; set; }
        
        // UniversitySiteEquipment
        public virtual List<UniversitySiteEquipment> Equipments { get; set; }
        
        // SiteCourseSchedule
        public virtual List<SiteCourseSchedule> SiteCourseSchedules { get; set; } = new();
        
        // Classroom
        public virtual List<Classroom> Classrooms { get; set; } = new();
    }
}
