using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class EquipmentTypeMapper
{
    public static EquipmentType ToDomainModel(this EquipmentTypeEntity equipmentTypeEntity)
    {
        return new EquipmentType(
            id: equipmentTypeEntity.EquipmentTypeId,
            name: equipmentTypeEntity.Name,
            code: equipmentTypeEntity.Code,
            description: equipmentTypeEntity.Description,
            createdAt: equipmentTypeEntity.CreatedAt,
            updatedAt: equipmentTypeEntity.UpdatedAt
        );
    }
    
    public static EquipmentTypeEntity ToEntity(this EquipmentType equipmentType)
    {
        return new EquipmentTypeEntity
        {
            EquipmentTypeId = equipmentType.Id,
            Name            = equipmentType.Name,
            Code            = equipmentType.Code,
            Description     = equipmentType.Description,
            CreatedAt       = equipmentType.CreatedAt,
            UpdatedAt       = equipmentType.UpdatedAt
        };
    }
}