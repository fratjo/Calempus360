#nullable enable
namespace Calempus360.Core.Models;

public class Equipment
{
    public Guid     Id          { get; private set; }
    public string   Name        { get; private set; }
    public string   Code        { get; private set; }
    public string   Brand       { get; private set; }
    public string   Model       { get; private set; }
    public string   Description { get; private set; }
    public DateTime CreatedAt   { get; private set; }
    public DateTime UpdatedAt   { get; private set; }

    // aggregates
    public EquipmentType? EquipmentType { get; private set; }

    // parent
    public Dictionary<string, University>? University { get; private set; }
    public Dictionary<string, Site>?       Site       { get; private set; }
    public Dictionary<string, Classroom>?  Classrooms { get; private set; }
    public List<Guid>?                     Sessions   { get; private set; }

    public Equipment(
        Guid                           id,
        string                         name,
        string                         code,
        string                         brand,
        string                         model,
        string                         description,
        DateTime                       createdAt,
        DateTime                       updatedAt,
        EquipmentType                  equipmentType,
        Dictionary<string, University> university,
        Dictionary<string, Site>?      site,
        Dictionary<string, Classroom>? classroom,
        List<Guid>                     sessions)
    {
        Id            = id;
        Name          = name;
        Code          = code;
        Brand         = brand;
        Model         = model;
        Description   = description;
        CreatedAt     = createdAt;
        UpdatedAt     = updatedAt;
        EquipmentType = equipmentType;
        University    = university;
        Site          = site;
        Classrooms    = classroom;
        Sessions      = sessions;
    }
}