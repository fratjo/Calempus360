using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.Equipment;

public interface IEquipmentService
{
    Task<IEnumerable<Models.EquipmentType>> GetEquipmentTypesAsync();
    Task<EquipmentType> GetEquipmentTypeByIdAsync(Guid id);
    Task<Models.EquipmentType> CreateEquipmentTypeAsync(Models.EquipmentType equipmentType);
    Task<Models.EquipmentType> UpdateEquipmentTypeAsync(Models.EquipmentType equipmentType);
    Task<bool> DeleteEquipmentTypeByIdAsync(Guid id);

    Task<IEnumerable<Models.Equipment>> GetEquipmentsAsync(Guid? universityId, Guid? siteId, Guid? classroomId, Guid? equipmentTypeId);
    Task<Models.Equipment> GetEquipmentByIdAsync(Guid id);
    Task<Models.Site> GetEquipmentSiteAsync(Guid id);

    Task<Models.Equipment> CreateEquipmentAsync(Models.Equipment equipment, Guid? equipmentTypeId, Guid? siteId,
        Guid universityId);

    Task<Models.Equipment> UpdateEquipmentAsync(Models.Equipment equipment, Guid? equipmentTypeId, Guid? classroomId, Guid academicYearId);
    Task<bool> ChangeEquipmentClassroomAsync(Guid equipmentId, Guid classroomId, Guid academciYearId);
    Task<bool> DeleteEquipmentAsync(Guid id);
    Task<bool> DeleteEquipmentsByUniversityAsync(Guid universityId);
    Task<bool> DeleteEquipmentsBySiteAsync(Guid siteId);
}