using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class StudentGroupMapper
{
    public static StudentGroup ToDomainModel(this StudentGroupEntity entity)
    {
        return new StudentGroup(
            id: entity.StudentGroupId,
            code: entity.Code,
            numberOfStudents: entity.NumberOfStudents,
            optionGrade: entity.OptionGrade,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            site: entity.SiteEntity?.ToDomainModel(),
            option: entity.OptionEntity?.ToDomainModel()
        );
    }

    public static StudentGroupEntity ToEntity(this StudentGroup model)
    {
        return new StudentGroupEntity
        {
            StudentGroupId   = model.Id,
            Code             = model.Code,
            NumberOfStudents = model.NumberOfStudents,
            OptionGrade      = model.OptionGrade,
            CreatedAt        = model.CreatedAt,
            UpdatedAt        = model.UpdatedAt,
            SiteEntity       = model.Site?.ToEntity(),
            OptionEntity     = model.Option?.ToEntity()
        };
    }
}