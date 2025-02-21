using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class SiteMapper
{
    public static Site ToDomainModel(this SiteEntity entity)
    {
        return new Site(
            id: entity.SiteId,
            name: entity.Name,
            code: entity.Code,
            address: entity.Address,
            phone: entity.Phone,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            classrooms: entity.Classrooms?
                              .Select(c => c.ToDomainModel())
                              .ToList(),
            schedules: entity.SiteCourseSchedules?
                             .Select(scs => scs.CourseScheduleEntity.ToDomainModel())
                             .ToList(),
            equipments: entity.Equipments?
                              .Select(e => e.EquipmentEntity.ToDomainModel())
                              .ToList()
        );
    }

    public static SiteEntity ToEntity(this Site model)
    {
        return new SiteEntity
        {
            SiteId    = model.Id,
            Name      = model.Name,
            Code      = model.Code,
            Address   = model.Address,
            Phone     = model.Phone,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
    }
}