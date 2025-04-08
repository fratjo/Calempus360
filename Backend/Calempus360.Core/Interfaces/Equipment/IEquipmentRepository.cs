using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.Equipment;

public interface IEquipmentRepository
{
    Task<IEnumerable<Models.EquipmentType>> GetEquipmentTypesAsync();
    Task<EquipmentType> GetEquipmentTypeByIdAsync(Guid? id);
    Task<Models.EquipmentType> CreateEquipmentTypeAsync(Models.EquipmentType equipmentType);
    Task<Models.EquipmentType> UpdateEquipmentTypeAsync(Models.EquipmentType equipmentType);
    Task<bool> DeleteEquipmentTypeByIdAsync(Guid id);

    Task<IEnumerable<Models.Equipment>> GetEquipmentsAsync(Guid? universityId, Guid? academicYearId, Guid? siteId, Guid? classroomId, Guid? equipmentTypeId, bool flying = false);
    Task<Models.Equipment> GetEquipmentByIdAsync(Guid id);
    Task<Models.Site> GetEquipmentSiteAsync(Guid id);
    Task<Models.Equipment> CreateEquipmentAsync(Models.Equipment equipment, Guid? siteId, Guid universityId);
    Task<Models.Equipment> UpdateEquipmentAsync(Models.Equipment equipment, Guid academicYearId);
    Task<bool> ChangeEquipmentClassroomAsync(Guid equipmentId, Guid classroomId, Guid academciYearId);
    Task<bool> DeleteEquipmentAsync(Guid id);
}