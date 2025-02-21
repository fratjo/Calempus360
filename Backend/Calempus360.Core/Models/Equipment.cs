#nullable enable
namespace Calempus360.Core.Models;

public class Equipment(
    Guid           id,
    string         name,
    string         code,
    string         brand,
    string         model,
    string         description,
    DateTime       createdAt,
    DateTime       updatedAt,
    EquipmentType? equipmentType)
{
    public Guid           Id            { get; private set; } = id;
    public string         Name          { get; private set; } = name;
    public string         Code          { get; private set; } = code;
    public string         Brand         { get; private set; } = brand;
    public string         Model         { get; private set; } = model;
    public string         Description   { get; private set; } = description;
    public DateTime       CreatedAt     { get; private set; } = createdAt;
    public DateTime       UpdatedAt     { get; private set; } = updatedAt;
    public EquipmentType? EquipmentType { get; private set; } = equipmentType;
}