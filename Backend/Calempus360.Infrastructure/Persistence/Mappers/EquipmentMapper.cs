using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class EquipmentMapper
{
    public static Equipment ToDomainModel(this EquipmentEntity equipmentEntity)
    {
        return new Equipment(
            id: equipmentEntity.EquipmentId,
            name: equipmentEntity.Name,
            code: equipmentEntity.Code,
            brand: equipmentEntity.Brand,
            model: equipmentEntity.Model,
            description: equipmentEntity.Description,
            createdAt: equipmentEntity.CreatedAt,
            updatedAt: equipmentEntity.UpdatedAt,
            equipmentType: equipmentEntity.EquipmentTypeEntity?.ToDomainModel()
        );
    }

    public static EquipmentEntity ToEntity(this Equipment equipment)
    {
        return new EquipmentEntity
        {
            EquipmentId                   = equipment.Id,
            Name                          = equipment.Name,
            Code                          = equipment.Code,
            Brand                         = equipment.Brand,
            Model                         = equipment.Model,
            Description                   = equipment.Description,
            CreatedAt                     = equipment.CreatedAt,
            UpdatedAt                     = equipment.UpdatedAt,
            EquipmentTypeEntity           = equipment.EquipmentType?.ToEntity() ?? null,
        };
    }
}