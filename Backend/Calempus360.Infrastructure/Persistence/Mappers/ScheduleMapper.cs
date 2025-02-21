using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;
using DayOfWeek = Calempus360.Core.Models.DayOfWeek;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class ScheduleMapper
{
    public static Schedule ToDomainModel(this CourseScheduleEntity entity)
    {
        return new Schedule(
            id: entity.ScheduleId,
            dayOfWeek: (DayOfWeek) entity.DayOfTheWeek,
            timeStart: entity.HourStart,
            timeEnd: entity.HourEnd
        );
    }
    
    public static CourseScheduleEntity ToEntity(this Schedule model)
    {
        return new CourseScheduleEntity
        {
            ScheduleId = model.Id,
            DayOfTheWeek = (int) model.DayOfWeek,
            HourStart = model.TimeStart,
            HourEnd = model.TimeEnd
        };
    }
}