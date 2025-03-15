#nullable enable
namespace Calempus360.Core.Models;

public class Equipment(
    string         name,
    string         code,        
    string         brand,
    string         model,
    string         description,
    Guid?          id            = null,
    DateTime?      createdAt     = null,
    DateTime?      updatedAt     = null,
    EquipmentType? equipmentType = null,
    Classroom?     classroom     = null
    )
{
    public Guid           Id            { get; private set; } = id ?? Guid.NewGuid();
    public string         Name          { get; private set; } = name;
    public string         Code          { get; private set; } = code;
    public string         Brand         { get; private set; } = brand;
    public string         Model         { get; private set; } = model;
    public string         Description   { get; private set; } = description;
    public DateTime       CreatedAt     { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime       UpdatedAt     { get; private set; } = updatedAt ?? DateTime.Now;
    public EquipmentType? EquipmentType { get; private set; } = equipmentType;
    public Classroom?     Classroom     { get; private set; } = classroom;
    
    public void SetEquipmentType(EquipmentType equipmentType) => EquipmentType = equipmentType;
    public void SetClassroom(Classroom classroom) => Classroom = classroom;
}