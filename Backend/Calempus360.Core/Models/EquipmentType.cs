namespace Calempus360.Core.Models;

public class EquipmentType(
    string   name,
    string   code,
    string   description,
    Guid?     id = null,
    DateTime? createdAt = null,
    DateTime? updatedAt = null)
{
    public Guid     Id          { get; private set; } = id ?? Guid.NewGuid();
    public string   Name        { get; private set; } = name;
    public string   Code        { get; private set; } = code;
    public string   Description { get; private set; } = description;
    public DateTime CreatedAt   { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime UpdatedAt   { get; private set; } = updatedAt ?? DateTime.Now;
}