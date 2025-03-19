using System.ComponentModel;
using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using ScheduleGenerator;

public class ScheduleService(Calempus360DbContext context)
{
    async void GenerateSchedule(Guid universityId, Guid academicYearId)
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

        var classrooms = string.Empty;
        var daysOfWeek = string.Empty;
        var hours = string.Empty;
        var courseGroups = string.Empty;
        var flyingEquipments = string.Empty;

        // var scheduler = new ScheduleGenerator.ScheduleGenerator(
        //     classrooms,
        //     daysOfWeek,
        //     hours,
        //     courseGroups,
        //     flyingEquipments
        // )
    }
}