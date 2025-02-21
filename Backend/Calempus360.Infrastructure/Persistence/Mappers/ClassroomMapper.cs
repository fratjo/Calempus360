using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class ClassroomMapper
{
    public static Classroom ToDomainModel(this ClassroomEntity entity)
    {
        return new Classroom(
            id: entity.ClassroomId,
            name: entity.Name,
            code: entity.Code,
            capacity: entity.Capacity,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            equipments: entity.ClassroomEquipments?
                              .Select(ce => ce.EquipmentEntity.ToDomainModel())
                              .ToList()
        );
    }

    public static ClassroomEntity ToEntity(this Classroom model)
    {
        return new ClassroomEntity
        {
            ClassroomId = model.Id,
            Name        = model.Name,
            Code        = model.Code,
            Capacity    = model.Capacity,
            CreatedAt   = model.CreatedAt,
            UpdatedAt   = model.UpdatedAt
        };
    }
}