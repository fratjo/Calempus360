using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class SessionMapper
{
    public static Session ToDomainModel(this SessionEntity entity)
    {
        return new Session(
            id: entity.SessionId,
            name: entity.Name,
            dateTimeStart: entity.DatetimeStart,
            dateTimeEnd: entity.DatetimeEnd,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            course: entity.CourseEntity.ToDomainModel(),
            classroom: entity.ClassroomEntity.ToDomainModel(),
            equipments: entity.EquipmentSessions?
                            .Select(es => es.EquipmentEntity.ToDomainModel())
                            .ToList(),
            studentGroups: entity.StudentGroupSessions?
                                .Select(sgs => sgs.StudentGroupEntity.ToDomainModel())
                                .ToList()
        );
    }
    
    public static SessionEntity ToEntity(this Session model)
    {
        return new SessionEntity
        {
            SessionId = model.Id,
            Name = model.Name,
            DatetimeStart = model.DateTimeStart,
            DatetimeEnd = model.DateTimeEnd,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
    }
}