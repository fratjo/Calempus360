using System.ComponentModel;
using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Core.Interfaces.Schedule;
using Calempus360.Core.Models;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
using Calempus360.Services.Adapters.ScheduleGenerator;
using Microsoft.EntityFrameworkCore;
using ScheduleGenerator;

public class ScheduleService(Calempus360DbContext context) : IScheduleService
{
    public async Task<bool> GenerateSchedule(Guid universityId, Guid academicYearId)
    {
        // Clean up current context for year and university
        var academicYear = await context.AcademicYears
            .FirstOrDefaultAsync(ay => ay.AcademicYearId == academicYearId);

        var x = from s in await context.Sessions
                    .Include(s => s.ClassroomEntity)
                    .ThenInclude(c => c.SiteEntity)
                    .Include(s => s.EquipmentSessions)
                    .Include(s => s.StudentGroupSessions)
                    .ToListAsync()
                where (
                    s.ClassroomEntity.SiteEntity!.UniversityId == universityId &&
                    (
                        s.DatetimeStart.Year == academicYear!.DateStart.Year ||
                        s.DatetimeEnd.Year == academicYear!.DateEnd.Year
                    )
                )
                select s;

        foreach (var session in x)
        {
            context.Sessions.Remove(session);
        }

        await context.SaveChangesAsync();

        var classrooms = from c in await context.Classrooms
                                .Include(c => c.ClassroomEquipments)!
                                    .ThenInclude(ce => ce.EquipmentEntity)
                                .Include(c => c.SiteEntity)
                                .Where(c => c.SiteEntity!.UniversityId == universityId)
                                .ToListAsync()
                         select ClassAdapter.Adapt(c);

        var flyingEquipments = from e in await context.Equipments
                                    .Include(e => e.EquipmentTypeEntity)
                                    .Include(e => e.UniversitySiteEquipmentEntity)
                                    .Where(e => e.UniversitySiteEquipmentEntity.UniversityId == universityId
                                                &&
                                                !context.ClassroomsEquipments.Any(
                                                    ce => ce.EquipmentId == e.EquipmentId &&
                                                    ce.AcademicYearId == academicYearId))
                                    .ToListAsync()
                               select EquipmentAdapter.Adapt(e);

        // partir depuis les groups puis option puis cours 
        // pour chaque groupe, on récupère les cours et on les ajoute à la liste des cours
        // ensuite on crée les groupes de cours
        var groups = await context.StudentGroups
                            .Include(g => g.OptionEntity)
                                .ThenInclude(o => o!.OptionCourses)
                                    .ThenInclude(oc => oc.CourseEntity)
                                        .ThenInclude(c => c.EquipmentTypes)
                                            .ThenInclude(et => et.EquipmentTypeEntity)
                            .Include(g => g.SiteEntity)
                            .Where(g => g.SiteEntity!.UniversityId == universityId && g.AcademicYearId == academicYearId)
                            .ToListAsync();

        // Pour chaque groupe, on filtre les OptionCourses afin de ne garder que ceux correspondant à l'OptionGrade du groupe
        foreach (var group in groups)
        {
            if (group.OptionEntity?.OptionCourses != null)
            {
                group.OptionEntity.OptionCourses = group.OptionEntity.OptionCourses
                    .Where(oc => oc.OptionGrade == group.OptionGrade)
                    .ToList();
            }
        }

        var courseGroupsLookup = groups
                                .SelectMany(g => g.OptionEntity?.OptionCourses
                                                .Select(oc => new { Course = oc.CourseEntity, Group = g })
                                                ?? Enumerable.Empty<dynamic>())
                                .GroupBy(x => x.Course)
                                .ToDictionary(g => g.Key, g => g.Select(x => x.Group).Distinct().ToList());

        var courseGroups = new List<CourseGroups>();

        foreach (var courseGroup in courseGroupsLookup)
        {
            courseGroups.Add(
                CourseGroupsAdapter
                    .Adapt(courseGroup.Key, courseGroup.Value.Cast<StudentGroupEntity>()
                    .ToList()));
        }

        var openings = from o in await context.SitesCoursesSchedules
                .Include(scs => scs.CourseScheduleEntity).ToListAsync()
                       from hour in Enumerable.Range(TimeOnlyToInt(o.CourseScheduleEntity.HourStart), TimeOnlyToInt(o.CourseScheduleEntity.HourEnd) - TimeOnlyToInt(o.CourseScheduleEntity.HourStart))
                       select (site: o.SiteEntity.SiteId.ToString() ?? string.Empty,
                           dayOfWeek: ((Calempus360.Core.Models.DayOfWeek)o.CourseScheduleEntity.DayOfTheWeek).ToString(),
                           startEndHour: (hour, hour + 1));
        // need to adapt the opening hours to the ScheduleGenerator format

        // var scheduler = new ScheduleGenerator.ScheduleGenerator(
        //     classrooms.ToList(),
        //     openings.ToList(),
        //     courseGroups.ToList(),
        //     flyingEquipments.ToList()
        // );

        return true;
    }

    static int TimeOnlyToInt(TimeOnly hour)
    {
        return hour.Hour;
    }
}