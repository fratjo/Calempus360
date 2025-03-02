namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class SiteEntity
    {
        public Guid? SiteId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // University
        public virtual Guid UniversityId { get; set; }
        public virtual UniversityEntity UniversityEntity { get; set; } = null!;
        
        // SiteAcademicYear
        public virtual List<SiteAcademicYearEntity> SiteAcademicYears { get; set; } 
        
        // StudentGroup
        public virtual List<StudentGroupEntity> StudentGroups { get; set; }
        
        // UniversitySiteEquipment
        public virtual List<UniversitySiteEquipmentEntity> Equipments { get; set; }
        
        // SiteCourseSchedule
        public virtual List<SiteCourseScheduleEntity> SiteCourseSchedules { get; set; } = new();
        
        // Classroom
        public virtual List<ClassroomEntity> Classrooms { get; set; } = new();
    }
}
