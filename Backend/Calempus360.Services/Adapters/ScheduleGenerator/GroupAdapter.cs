using Calempus360.Core.Models;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class GroupAdapter
{
    public static Group Adapt(StudentGroup group)
    {
        return new Group
        {
            Name = group.Code,
            Capacity = group.NumberOfStudents,
            PreferedSite = group.Site.Name
        };
    }
}