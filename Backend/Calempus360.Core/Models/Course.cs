namespace Calempus360.Core.Models;

public class Course(
    Guid                 id,
    string               name,
    string               code,
    string               description,
    int                  totalHours,
    int                  weeklyHours,
    string               semester,
    int                  credits,
    DateTime             createdAt,
    DateTime             updatedAt,
    List<EquipmentType>? equipmentTypes)
{
    public Guid                 Id             { get; private set; } = id;
    public string               Name           { get; private set; } = name;
    public string               Code           { get; private set; } = code;
    public string               Description    { get; private set; } = description;
    public int                  TotalHours     { get; private set; } = totalHours;
    public int                  WeeklyHours    { get; private set; } = weeklyHours;
    public string               Semester       { get; private set; } = semester;
    public int                  Credits        { get; private set; } = credits;
    public DateTime             CreatedAt      { get; private set; } = createdAt;
    public DateTime             UpdatedAt      { get; private set; } = updatedAt;
    public List<EquipmentType>? EquipmentTypes { get; private set; } = equipmentTypes;
}