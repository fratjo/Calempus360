namespace Calempus360.Core.Models;

public class StudentGroupSession
{
    // StudentGroup
    public int StudentGroupId { get; set; }
    public virtual StudentGroup StudentGroup { get; set; } = null!;
    
    // Session
    public int SessionId { get; set; }
    public virtual Session Session { get; set; } = null!;
}