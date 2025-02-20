namespace Calempus360.Core.Models;

public class Classroom
{
    public Guid     Id        { get; private set; }
    public string   Name      { get; private set; }
    public string   Code      { get; private set; }
    public string   Address   { get; private set; }
    public int      Capacity  { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // parent
    public Guid? SiteId { get; private set; }

    // aggregates
    public Dictionary<string, List<Equipment>>? Equipments { get; private set; }

    public List<Session>? Sessions { get; private set; }

    public Classroom(
        Guid                                 id,
        string                               name,
        string                               code,
        string                               address,
        int                                  capacity,
        DateTime                             createdAt,
        DateTime                             updatedAt,
        Guid                                 siteId,
        Dictionary<string, List<Equipment>>? equipments,
        List<Session>                        sessions)
    {
        Id         = id;
        Name       = name;
        Code       = code;
        Address    = address;
        Capacity   = capacity;
        CreatedAt  = createdAt;
        UpdatedAt  = updatedAt;
        SiteId     = siteId;
        Equipments = equipments;
        Sessions   = sessions;
    }
}