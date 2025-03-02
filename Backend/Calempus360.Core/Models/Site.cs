namespace Calempus360.Core.Models;

public class Site(
    Guid?            id,
    string           name,
    string           code,
    string           address,
    string           phone,
    DateTime?        createdAt,
    DateTime?        updatedAt,
    List<Classroom>? classrooms,
    List<Schedule>?  schedules,
    List<Equipment>? equipments)
{
    public Guid?            Id         { get; private set; } = id;
    public string           Name       { get; private set; } = name;
    public string           Code       { get; private set; } = code;
    public string           Address    { get; private set; } = address;
    public string           Phone      { get; private set; } = phone;
    public DateTime?        CreatedAt  { get; private set; } = createdAt;
    public DateTime?        UpdatedAt  { get; private set; } = updatedAt;
    public List<Classroom>? Classrooms { get; private set; } = classrooms;
    public List<Schedule>?  Schedules  { get; private set; } = schedules;
    public List<Equipment>? Equipments { get; private set; } = equipments;
}