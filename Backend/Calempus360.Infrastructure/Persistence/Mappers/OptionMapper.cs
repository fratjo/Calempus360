using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class OptionMapper
{
    public static Option ToDomainModel(this OptionEntity entity)
    {
        return new Option(
            id: entity.OptionId,
            name: entity.Name,
            code: entity.Code,
            description: entity.Description,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            courses: entity.OptionCourses?
                        .Select(oc => oc.CourseEntity.ToDomainModel())
                        .ToList()
        );
    }
    
    public static OptionEntity ToEntity(this Option model)
    {
        return new OptionEntity
        {
            OptionId   = model.Id,
            Name       = model.Name,
            Code       = model.Code,
            Description = model.Description,
            CreatedAt  = model.CreatedAt,
            UpdatedAt  = model.UpdatedAt
        };
    }
}