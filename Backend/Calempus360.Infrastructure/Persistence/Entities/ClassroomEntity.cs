namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class ClassroomEntity
    {
        public Guid ClassroomId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties

        // Site
        public Guid? SiteId { get; set; }
        public virtual SiteEntity? SiteEntity { get; set; } = null!;

        // ClassroomEquipment
        public IEnumerable<ClassroomEquipmentEntity> ClassroomEquipments { get; set; } = null!;

        // Session
        public List<SessionEntity>? Sessions { get; set; }

    }
}
