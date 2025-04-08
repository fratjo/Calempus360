using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class EquipmentAdapter
{
    public static Equipement Adapt(EquipmentEntity equipment)
    {
        return new Equipement(
            site: equipment.UniversitySiteEquipmentEntity.SiteEntity!.SiteId!.ToString(),
            type: equipment.EquipmentTypeEntity!.EquipmentTypeId!.ToString(),
            code: equipment.EquipmentId
        );
    }
}