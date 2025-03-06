using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.Equipment;

public interface IEquipmentRepository
{
    Task<IEnumerable<Models.EquipmentType>> GetEquipmentTypesAsync();
    Task<EquipmentType>                     GetEquipmentTypeByIdAsync(Guid                id);
    Task<Models.EquipmentType>              CreateEquipmentTypeAsync(Models.EquipmentType equipmentType);
    Task<Models.EquipmentType>              UpdateEquipmentTypeAsync(Models.EquipmentType equipmentType);
    Task<bool>                              DeleteEquipmentTypeByIdAsync(Guid             id);

    Task<IEnumerable<Models.Equipment>> GetEquipmentsByUniversityAsync(Guid   universityId);
    Task<IEnumerable<Models.Equipment>> GetEquipmentsBySiteAsync(Guid         siteId);
    Task<IEnumerable<Models.Equipment>> GetEquipmentsByClassroomIdAsync(Guid  classroomId);
    Task<Models.Equipment>              GetEquipmentByIdAsync(Guid            id);
    Task<Models.Equipment>              CreateEquipmentAsync(Models.Equipment equipment, Guid siteId, Guid universityId);
    Task<Models.Equipment>              CreateEquipmentAsync(Models.Equipment equipment, Guid universityId);
    Task<Models.Equipment>              UpdateEquipmentAsync(Models.Equipment equipment);
    Task<bool>                          DeleteEquipmentAsync(Guid             id);
}