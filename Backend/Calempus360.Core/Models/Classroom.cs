namespace Calempus360.Core.Models;

public class Classroom(
    Guid             id,
    string           name,
    string           code,
    int              capacity,
    DateTime         createdAt,
    DateTime         updatedAt,
    List<Equipment>? equipments)
{
    public Guid             Id         { get; private set; } = id;
    public string           Name       { get; private set; } = name;
    public string           Code       { get; private set; } = code;
    public int              Capacity   { get; private set; } = capacity;
    public DateTime         CreatedAt  { get; private set; } = createdAt;
    public DateTime         UpdatedAt  { get; private set; } = updatedAt;
    public List<Equipment>? Equipments { get; private set; } = equipments;
}