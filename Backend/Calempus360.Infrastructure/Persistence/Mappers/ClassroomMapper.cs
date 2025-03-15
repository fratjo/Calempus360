using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class ClassroomMapper
{
    public static Classroom ToDomainModel(this ClassroomEntity entity, bool includeEquipments = false)
    {
        return new Classroom(
            id: entity.ClassroomId,
            name: entity.Name,
            code: entity.Code,
            capacity: entity.Capacity,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            equipments: includeEquipments ? entity.ClassroomEquipments?
                              .Select(ce => ce.EquipmentEntity.ToDomainModel())
                              .ToList() : null,
            site: entity.SiteId
        );
    }

    public static ClassroomEntity ToEntity(this Classroom model)
    {
        return new ClassroomEntity
        {
            ClassroomId = model.Id,
            Name = model.Name,
            Code = model.Code,
            Capacity = model.Capacity,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
    }
}