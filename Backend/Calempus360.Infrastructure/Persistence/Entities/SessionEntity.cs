namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class SessionEntity
    {
        public Guid SessionId { get; set; }
        public string Name { get; set; }
        public DateTime DatetimeStart { get; set; }
        public DateTime DatetimeEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // Classroom
        public Guid ClassroomId { get; set; }
        public virtual ClassroomEntity ClassroomEntity { get; set; } = null!;
        
        // Course
        public Guid CourseId { get; set; }
        public virtual CourseEntity CourseEntity { get; set; } = null!;
        
        //  EquipmentSessions
        public virtual List<EquipmentSessionEntity> EquipmentSessions { get; set; }
        
        // StudentGroupSessions
        public virtual List<StudentGroupSessionEntity> StudentGroupSessions { get; set; }
    }
}
