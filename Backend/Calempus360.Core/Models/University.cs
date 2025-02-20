namespace Calempus360.Core.Models;

public class University
{
    public Guid     Id        { get; private set; }
    public string   Name      { get; private set; }
    public string   Code      { get; private set; }
    public string   Phone     { get; private set; }
    public string   Address   { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // compositions
    public Dictionary<string, List<Site>>      Sites      { get; private set; }
    public Dictionary<string, List<Equipment>>? Equipments { get; private set; }

    public University(
        Guid                                 id,
        string                               name,
        string                               code,
        string                               phone,
        string                               address,
        DateTime                             createdAt,
        DateTime                             updatedAt,
        Dictionary<string, List<Site>>?      sites,
        Dictionary<string, List<Equipment>>? equipments)
    {
        Id         = id;
        Name       = name;
        Code       = code;
        Phone      = phone;
        Address    = address;
        CreatedAt  = createdAt;
        UpdatedAt  = updatedAt;
        Sites      = sites;
        Equipments = equipments;
    }
}