using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class EquipmentResponseDto
{
    public Guid?   Id   { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public EquipmentTypeResponseDto? EquipmentType { get; set; }
}

public static partial class DtoMapper
{
    public static EquipmentResponseDto MapToDto(this Equipment equipment)
    {
        return new EquipmentResponseDto
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Code = equipment.Code,
            Brand = equipment.Brand,
            Model = equipment.Model,
            Description = equipment.Description,
            CreatedAt = equipment.CreatedAt,
            UpdatedAt = equipment.UpdatedAt,
            EquipmentType = equipment.EquipmentType?.MapToDto()
        };
    }
}

public class EquipmentTypeResponseDto
{
    public Guid?    Id          { get; set; }
    public string?  Name        { get; set; }
    public string?  Code        { get; set; }
    public string?  Description { get; set; }
    public DateTime? CreatedAt   { get; set; }
    public DateTime? UpdatedAt   { get; set; }
}

public static partial class DtoMapper
{
    public static EquipmentTypeResponseDto MapToDto(this EquipmentType equipmentType)
    {
        return new EquipmentTypeResponseDto
        {
            Id = equipmentType.Id,
            Name = equipmentType.Name,
            Code = equipmentType.Code,
            Description = equipmentType.Description,
            CreatedAt = equipmentType.CreatedAt,
            UpdatedAt = equipmentType.UpdatedAt
        };
    }
}