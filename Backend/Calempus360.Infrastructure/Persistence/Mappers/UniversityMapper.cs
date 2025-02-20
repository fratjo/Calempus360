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
                         .GroupBy(s => s.AcademicYearId)
                         .ToDictionary(
                              g => g.Key,
                              g => g.Select(site => new Site(
                                                id: site.SiteId,
                                                name: site.Name,
                                                code: site.Code,
                                                address: site.Address,
                                                phone: site.Phone,
                                                createdAt: site.CreatedAt,
                                                updatedAt: site.UpdatedAt,
                                                university: null,
                                                classrooms: null,
                                                schedules: null,
                                                equipments: null,
                                                studentGroups: null
                                            )).ToList()
                          ),
            equipments: entity.Equipments?
                              .GroupBy(e => e.EquipmentEntity.EquipmentTypeEntity.Name)
                              .ToDictionary(
                                   g => g.Key,
                                   g => g.Select(equipment => new Equipment(
                                                     id: equipment.EquipmentId,
                                                     name: equipment.EquipmentEntity.Name,
                                                     code: equipment.EquipmentEntity.Code,
                                                     brand: equipment.EquipmentEntity.Brand,
                                                     model: equipment.EquipmentEntity.Model,
                                                     description: equipment.EquipmentEntity.Description,
                                                     createdAt: equipment.EquipmentEntity.CreatedAt,
                                                     updatedAt: equipment.EquipmentEntity.UpdatedAt,
                                                     equipmentType: null,
                                                     university: null,
                                                     site: null,
                                                     classroom: null,
                                                     sessions: null
                                                 )).ToList()
                               )
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