namespace Calempus360.Core.Models;

public class Session(
    string              name,
    DateTime            dateTimeStart,
    DateTime            dateTimeEnd,
    Guid?               id            = null,
    DateTime?           createdAt     = null,
    DateTime?           updatedAt     = null,
    List<Equipment>?    equipments    = null,
    List<StudentGroup>? studentGroups = null,
    Course?             course        = null,
    Classroom?          classroom     = null)
{
    public Guid                Id            { get; private set; } = id ?? Guid.NewGuid();
    public string              Name          { get; private set; } = name;
    public DateTime            DateTimeStart { get; private set; } = dateTimeStart;
    public DateTime            DateTimeEnd   { get; private set; } = dateTimeEnd;
    public DateTime            CreatedAt     { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime            UpdatedAt     { get; private set; } = updatedAt ?? DateTime.Now;
    public List<Equipment>?    Equipments    { get; private set; } = equipments;
    public List<StudentGroup>? StudentGroups { get; private set; } = studentGroups;
    public Course?             Course        { get; private set; } = course;
    public Classroom?           Classroom     { get; private set; } = classroom;
}