namespace Calempus360.Core.Models;

public class Option
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public List<StudentGroup> StudentGroups { get; private set; }
    public Dictionary<string,List<Course>> Courses { get; private set; }
    
    public Option(
        Guid     id,
        string   name,
        string   code,
        string   description,
        DateTime createdAt,
        DateTime updatedAt,
        List<StudentGroup> studentGroups,
        Dictionary<string,List<Course>> courses)
    {
        Id           = id;
        Name         = name;
        Code         = code;
        Description  = description;
        CreatedAt    = createdAt;
        UpdatedAt    = updatedAt;
        StudentGroups = studentGroups;
        Courses      = courses;
    }
}