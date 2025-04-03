using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class CourseGroupsAdapter
{
    public static CourseGroups Adapt(CourseEntity course, List<StudentGroupEntity> groups)
    {
        var courseGroups = new CourseGroups
        {
            Course = course.CourseId!.ToString(),
            Groups = groups.Select(g => new Group()
            {
                Name = g.StudentGroupId!.ToString(),
                Capacity = g.NumberOfStudents,
                PreferedSite = g.SiteEntity!.SiteId!.ToString()!,
            }
                ).ToList(),
            Equipements = course.EquipmentTypes
                                .Select(et =>
                                            new Equipement(
                                                null,
                                                et.EquipmentTypeEntity.EquipmentTypeId!.ToString(),
                                                null))
                                .ToList()
        };

        return courseGroups;
    }
}