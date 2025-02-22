using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class AcademicYearMapper
{
    public static AcademicYear ToDomainModel(this AcademicYearEntity entity)
    {
        return new AcademicYear(
            id: entity.AcademicYearId,
            dateStart: entity.DateStart,
            dateEnd: entity.DateEnd,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            daysWithoutCourses: entity.DaysWithoutCourses?
                                  .Select(dw => dw.ToDomainModel())
                                  .ToList()
        );
    }
    
    public static AcademicYearEntity ToEntity(this AcademicYear model)
    {
        return new AcademicYearEntity
        {
            AcademicYearId = model.Id,
            DateStart      = model.DateStart,
            DateEnd        = model.DateEnd,
            CreatedAt      = model.CreatedAt,
            UpdatedAt      = model.UpdatedAt
        };
    }
}