using Calempus360.Core.Models;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class CourseGroupsAdapter
{
    public static CourseGroups Adapt(Course course, List<StudentGroup> groups)
    {
        var courseGroups = new CourseGroups
        {
            Course = course.Name,
            Groups = groups.Select(g => new Group()
                {
                    Name = g.Code,
                    Capacity = g.NumberOfStudents,
                    PreferedSite = g.Site.Code
                }
                ).ToList(),
            Equipements = course.EquipmentTypes
                                .Select(et => 
                                            new Equipement(
                                                null, 
                                                et.EquipmentType.Name, 
                                                null))
                                .ToList()
        };

        return courseGroups;
    }
}