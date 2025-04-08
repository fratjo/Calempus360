namespace Calempus360.Infrastructure.Persistence.Entities;

public class EquipmentSessionEntity
{
    // Equipment
    public Guid EquipmentId { get; set; }
    public virtual EquipmentEntity EquipmentEntity { get; set; } = null!;
    
    // Session
    public Guid SessionId { get; set; }
    public virtual SessionEntity SessionEntity { get; set; } = null!;
}