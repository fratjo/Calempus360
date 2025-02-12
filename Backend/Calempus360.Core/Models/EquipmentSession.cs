namespace Calempus360.Core.Models;

public class EquipmentSession
{
    // Equipment
    public int EquipmentId { get; set; }
    public virtual Equipment Equipment { get; set; } = null!;
    
    // Session
    public int SessionId { get; set; }
    public virtual Session Session { get; set; } = null!;
}