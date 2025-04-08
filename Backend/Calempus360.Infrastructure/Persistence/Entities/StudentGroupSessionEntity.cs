namespace Calempus360.Infrastructure.Persistence.Entities;

public class StudentGroupSessionEntity
{
    // StudentGroup
    public Guid StudentGroupId { get; set; }
    public virtual StudentGroupEntity StudentGroupEntity { get; set; } = null!;
    
    // Session
    public Guid SessionId { get; set; }
    public virtual SessionEntity SessionEntity { get; set; } = null!;
}