namespace Calempus360.Core.Models;

public class Session
{
    public Guid     Id            { get; private set; }
    public string   Name          { get; private set; }
    public DateTime DateTimeStart { get; private set; }
    public DateTime DateTimeEnd   { get; private set; }
    public DateTime CreatedAt     { get; private set; }
    public DateTime UpdatedAt     { get; private set; }
    
    // aggregates
    public List<Equipment> Equipments { get; private set; }
    public List<StudentGroup> StudentGroups { get; private set; }
    public Course Course { get; private set; }
    public Classroom Classroom { get; private set; }
    
    public Session(
        Guid           id,
        string         name,
        DateTime       dateTimeStart,
        DateTime       dateTimeEnd,
        DateTime       createdAt,
        DateTime       updatedAt,
        List<Equipment> equipments,
        List<StudentGroup> studentGroups,
        Course         course,
        Classroom      classroom)
    {
        Id            = id;
        Name          = name;
        DateTimeStart = dateTimeStart;
        DateTimeEnd   = dateTimeEnd;
        CreatedAt     = createdAt;
        UpdatedAt     = updatedAt;
        Equipments    = equipments;
        StudentGroups = studentGroups;
        Course        = course;
        Classroom     = classroom;
    }
}