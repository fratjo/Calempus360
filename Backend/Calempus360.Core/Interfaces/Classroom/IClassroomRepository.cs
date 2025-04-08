namespace Calempus360.Core.Interfaces.Classroom;

public interface IClassroomRepository
{
    Task<IEnumerable<Models.Classroom>> GetClassroomsAsync(Guid? universityId, Guid? siteId);
    Task<Models.Classroom> GetClassroomByIdAsync(Guid id);
    Task<Models.Classroom> CreateClassroomAsync(Models.Classroom classroom, Guid siteId);
    Task<bool> AddEquipmentToClassroomAsync(Guid classroomId, Guid equipmentId, Guid academicYearId);
    Task<Models.Classroom> UpdateClassroomAsync(Models.Classroom classroom);
    Task<bool> DeleteClassroomAsync(Guid id);
    Task<bool> DeleteClassroomsBySiteAsync(Guid siteId);
    Task<bool> RemoveEquipmentFromClassroomAsync(Guid classroomId, Guid equipmentId, Guid academicYearId);
}