namespace Calempus360.Core.Models;

public class Course
{
    public Guid     Id          { get; private set; }
    public string   Name        { get; private set; }
    public string   Code        { get; private set; }
    public string   Description { get; private set; }
    public int      TotalHours  { get; private set; }
    public int      WeeklyHours { get; private set; }
    public string   Semester    { get; private set; }
    public int      Credits     { get; private set; }
    public DateTime CreatedAt   { get; private set; }
    public DateTime UpdatedAt   { get; private set; }

    // aggregates
    public Dictionary<(string acadYear, Guid universityId), List<EquipmentType>>? EquipmentTypes { get; private set; }
    
    // parent
    public Dictionary<string, List<Option>>? Options  { get; private set; }
    public List<Session>?                    Sessions { get; private set; }
    
    public Course(
        Guid                                 id,
        string                               name,
        string                               code,
        string                               description,
        int                                  totalHours,
        int                                  weeklyHours,
        string                               semester,
        int                                  credits,
        DateTime                             createdAt,
        DateTime                             updatedAt,
        Dictionary<(string acadYear, Guid universityId), List<EquipmentType>>? equipmentTypes,
        Dictionary<string, List<Option>>      options,
        List<Session>                        sessions)
    {
        Id          = id;
        Name        = name;
        Code        = code;
        Description = description;
        TotalHours  = totalHours;
        WeeklyHours = weeklyHours;
        Semester    = semester;
        Credits     = credits;
        CreatedAt   = createdAt;
        UpdatedAt   = updatedAt;
        EquipmentTypes = equipmentTypes;
        Options        = options;
        Sessions       = sessions;
    }
}