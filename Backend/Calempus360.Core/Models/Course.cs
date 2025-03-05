namespace Calempus360.Core.Models;

public class Course(
    string               name,
    string               code,
    string               description,
    int                  totalHours,
    int                  weeklyHours,
    string               semester,
    int                  credits,
    Guid?                 id = null,
    DateTime?             createdAt = null,
    DateTime?             updatedAt = null,
    List<EquipmentType>? equipmentTypes = null)
{
    public Guid                 Id             { get; private set; } = id ?? Guid.NewGuid();
    public string               Name           { get; private set; } = name;
    public string               Code           { get; private set; } = code;
    public string               Description    { get; private set; } = description;
    public int                  TotalHours     { get; private set; } = totalHours;
    public int                  WeeklyHours    { get; private set; } = weeklyHours;
    public string               Semester       { get; private set; } = semester;
    public int                  Credits        { get; private set; } = credits;
    public DateTime             CreatedAt      { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime             UpdatedAt      { get; private set; } = updatedAt ?? DateTime.Now;
    public List<EquipmentType>? EquipmentTypes { get; private set; } = equipmentTypes;
}