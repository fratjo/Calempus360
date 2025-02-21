namespace Calempus360.Core.Models;

public class EquipmentType(
    Guid     id,
    string   name,
    string   code,
    string   description,
    DateTime createdAt,
    DateTime updatedAt)
{
    public Guid     Id          { get; private set; } = id;
    public string   Name        { get; private set; } = name;
    public string   Code        { get; private set; } = code;
    public string   Description { get; private set; } = description;
    public DateTime CreatedAt   { get; private set; } = createdAt;
    public DateTime UpdatedAt   { get; private set; } = updatedAt;
}