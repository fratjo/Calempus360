using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;

namespace Calempus360.Infrastructure.Persistence.Mappers;

public static class UniversityMapper
{
    // Mapper vers le domaine, est plus complexe car il y a des groupements à faire
    public static University ToDomainModel(this UniversityEntity entity)
    {
        return new University(
            id: entity.UniversityId,
            name: entity.Name,
            code: entity.Code,
            phone: entity.Phone,
            address: entity.Address,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            sites: entity.Sites?
                         .Select(s => s.ToDomainModel())
                         .ToList(),
            equipments: entity.Equipments?
                              .Select(e => e.EquipmentEntity.ToDomainModel())
                              .ToList()
        );
    }

    // Mapper vers le repository, est plus simple car c'est un objet plat utile pour l'insertion en base de données
    public static UniversityEntity ToEntity(this University model)
    {
        return new UniversityEntity()
        {
            UniversityId = model.Id,
            Name         = model.Name,
            Code         = model.Code,
            Phone        = model.Phone,
            Address      = model.Address,
            CreatedAt    = model.CreatedAt,
            UpdatedAt    = model.UpdatedAt,
        };
    }
}