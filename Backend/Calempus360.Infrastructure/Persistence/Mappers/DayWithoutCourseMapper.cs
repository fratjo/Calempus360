using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class DayWithoutCourseMapper
{
    public static DayWithoutCourse ToDomainModel(this DayWithoutCourseEntity entity)
    {
        return new DayWithoutCourse(
            id: entity.DayWithoutCourseId,
            name: entity.Name,
            date: entity.Date,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt
        );
    }

    public static DayWithoutCourseEntity ToEntity(this DayWithoutCourse model)
    {
        return new DayWithoutCourseEntity
        {
            DayWithoutCourseId = model.Id,
            Name               = model.Name,
            Date               = model.Date,
            CreatedAt          = model.CreatedAt,
            UpdatedAt          = model.UpdatedAt
        };
    }
}