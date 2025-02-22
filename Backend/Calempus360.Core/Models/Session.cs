namespace Calempus360.Core.Models;

public class Session(
    Guid                id,
    string              name,
    DateTime            dateTimeStart,
    DateTime            dateTimeEnd,
    DateTime            createdAt,
    DateTime            updatedAt,
    List<Equipment>?    equipments,
    List<StudentGroup>? studentGroups,
    Course              course,
    Classroom           classroom)
{
    public Guid                Id            { get; private set; } = id;
    public string              Name          { get; private set; } = name;
    public DateTime            DateTimeStart { get; private set; } = dateTimeStart;
    public DateTime            DateTimeEnd   { get; private set; } = dateTimeEnd;
    public DateTime            CreatedAt     { get; private set; } = createdAt;
    public DateTime            UpdatedAt     { get; private set; } = updatedAt;
    public List<Equipment>?    Equipments    { get; private set; } = equipments;
    public List<StudentGroup>? StudentGroups { get; private set; } = studentGroups;
    public Course              Course        { get; private set; } = course;
    public Classroom           Classroom     { get; private set; } = classroom;
}