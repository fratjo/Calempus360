using Calempus360.Core.Models;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class EquipmentAdapter
{
    public static Equipement Adapt(Equipment equipment)
    {
        return new Equipement(
            equipment.UniversitySiteEquipment.Site.Name,
            equipment.EquipmentType.Name,
            equipment.EquipmentId
        );
    }
}