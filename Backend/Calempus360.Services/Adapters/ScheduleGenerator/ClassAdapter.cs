using Calempus360.Infrastructure.Persistence.Entities;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class ClassAdapter
{
    public static Class Adapt(ClassroomEntity classroom)
    {
        var room = new Class(
            classroom.ClassroomId!.ToString()!,
            classroom.SiteEntity!.SiteId!.ToString()!,
            classroom.Capacity,
            classroom.ClassroomEquipments!
                .Select(ce =>
                            new Equipement(
                                classroom.SiteEntity.SiteId!.ToString(),
                                ce.EquipmentEntity.EquipmentTypeEntity!.EquipmentTypeId!.ToString(),
                                ce.EquipmentEntity.EquipmentId!))
                .ToList()
        );
        return room;
    }
}