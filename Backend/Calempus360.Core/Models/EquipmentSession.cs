namespace Calempus360.Core.Models;

public class EquipmentSession
{
    // Equipment
    public Guid EquipmentId { get; set; }
    public virtual Equipment Equipment { get; set; } = null!;
    
    // Session
    public Guid SessionId { get; set; }
    public virtual Session Session { get; set; } = null!;
}