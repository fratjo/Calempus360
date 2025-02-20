using Calempus360.Infrastructure.Persistence.Entities;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class ClassAdapter
{
    public static Class Adapt(ClassroomEntity classroom)
    {   
        var room = new Class(
            classroom.Name,
            classroom.SiteEntity.Name,
            classroom.Capacity,
            classroom.ClassroomEquipments
                .Select(ce => 
                            new Equipement(
                                classroom.SiteEntity.Name, 
                                ce.EquipmentEntity.EquipmentTypeEntity!.Name, 
                                ce.EquipmentEntity.EquipmentId!))
                .ToList()
        );
        return room;
    }
}