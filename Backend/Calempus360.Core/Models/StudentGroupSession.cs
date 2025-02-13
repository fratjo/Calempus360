namespace Calempus360.Core.Models;

public class StudentGroupSession
{
    // StudentGroup
    public Guid StudentGroupId { get; set; }
    public virtual StudentGroup StudentGroup { get; set; } = null!;
    
    // Session
    public Guid SessionId { get; set; }
    public virtual Session Session { get; set; } = null!;
}