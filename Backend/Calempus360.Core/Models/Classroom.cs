namespace Calempus360.Core.Models;

public class Classroom(
    string           name,
    string           code,
    int              capacity,
    Guid?            id         = null,
    DateTime?        createdAt  = null,
    DateTime?        updatedAt  = null,
    List<Equipment>? equipments = null)
{
    public Guid             Id         { get; private set; } = id ?? Guid.NewGuid();
    public string           Name       { get; private set; } = name;
    public string           Code       { get; private set; } = code;
    public int              Capacity   { get; private set; } = capacity;
    public DateTime         CreatedAt  { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime         UpdatedAt  { get; private set; } = updatedAt ?? DateTime.Now;
    public List<Equipment>? Equipments { get; private set; } = equipments;
}