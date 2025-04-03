using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;
using ScheduleGenerator;

namespace Calempus360.Services.Adapters.ScheduleGenerator;

public static class GroupAdapter
{
    public static Group Adapt(StudentGroupEntity group)
    {
        return new Group
        {
            Name = group.StudentGroupId!.ToString(),
            Capacity = group.NumberOfStudents,
            PreferedSite = group.SiteEntity!.SiteId!.ToString()!
        };
    }
}