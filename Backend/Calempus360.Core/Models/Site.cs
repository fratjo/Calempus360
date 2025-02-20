namespace Calempus360.Core.Models;

public class Site
{
    public Guid     Id        { get; private set; }
    public string   Name      { get; private set; }
    public string   Code      { get; private set; }
    public string   Address   { get; private set; }
    public string   Phone     { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // parent
    public University? University { get; private set; }

    // compositions
    public Dictionary<string, List<Classroom>>? Classrooms { get; private set; }

    // aggregates
    public Dictionary<string, List<Schedule>>?  Schedules  { get; private set; }
    public Dictionary<string, List<Equipment>>? Equipments { get; private set; }

    public List<string>? StudentGroups { get; private set; }

    public Site(
        Guid                                 id,
        string                               name,
        string                               code,
        string                               address,
        string                               phone,
        DateTime                             createdAt,
        DateTime                             updatedAt,
        University                           university,
        Dictionary<string, List<Classroom>>  classrooms,
        Dictionary<string, List<Schedule>>?  schedules,
        Dictionary<string, List<Equipment>>? equipments,
        List<string>?                        studentGroups)
    {
        Id            = id;
        Name          = name;
        Code          = code;
        Address       = address;
        Phone         = phone;
        CreatedAt     = createdAt;
        UpdatedAt     = updatedAt;
        University    = university;
        Classrooms    = classrooms;
        Schedules     = schedules;
        Equipments    = equipments;
        StudentGroups = studentGroups;
    }
}