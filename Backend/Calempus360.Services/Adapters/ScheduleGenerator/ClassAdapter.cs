using Calempus360.Core.Models;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class ClassAdapter
{
    public static Class Adapt(Classroom classroom)
    {   
        var room = new Class(
            classroom.Name,
            classroom.Site.Name,
            classroom.Capacity,
            classroom.ClassroomEquipments
                .Select(ce => 
                            new Equipement(
                                classroom.Site.Name, 
                                ce.Equipment.EquipmentType!.Name, 
                                ce.Equipment.EquipmentId!))
                .ToList()
        );
        return room;
    }
}