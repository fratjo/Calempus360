namespace Calempus360.Core.Models;

public class EquipmentType
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public EquipmentType(
        Guid     id,
        string   name,
        string   code,
        string   description,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id          = id;
        Name        = name;
        Code        = code;
        Description = description;
        CreatedAt   = createdAt;
        UpdatedAt   = updatedAt;
    }
}