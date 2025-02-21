using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class CourseMapper
{
    public static Course ToDomainModel(this CourseEntity entity)
    {
        return new Course(
            id: entity.CourseId,
            name: entity.Name,
            code: entity.Code,
            description: entity.Description,
            weeklyHours: entity.WeeklyHours,
            totalHours: entity.TotalHours,
            semester: entity.Semester,
            credits: entity.Credits,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            equipmentTypes: entity.EquipmentTypes?
                                .Select(cet => cet.EquipmentTypeEntity.ToDomainModel())
                                .ToList()
        );
    }
    
    public static CourseEntity ToEntity(this Course model)
    {
        return new CourseEntity
        {
            CourseId   = model.Id,
            Name       = model.Name,
            Code       = model.Code,
            Description = model.Description,
            WeeklyHours = model.WeeklyHours,
            TotalHours = model.TotalHours,
            Semester = model.Semester,
            Credits = model.Credits,
            CreatedAt  = model.CreatedAt,
            UpdatedAt  = model.UpdatedAt
        };
    }
}