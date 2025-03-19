using System.ComponentModel;
using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Calempus360.Services.Adapters.ScheduleGenerator;
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

        var classrooms = from c in await context.Classrooms.Include(c => c.ClassroomEquipments)!.ThenInclude(ce => ce.EquipmentEntity).ToListAsync() select ClassAdapter.Adapt(c);

        var flyingEquipments = from e in await context.Equipments
                                    .Include(e => e.EquipmentTypeEntity)
                                    .Where(e => !context.ClassroomsEquipments
                                    .Any(
                                        ce => ce.EquipmentId == e.EquipmentId &&
                                        ce.AcademicYearId == academicYearId))
                                    .ToListAsync()
                               select EquipmentAdapter.Adapt(e);

        var courseGroups = string.Empty;
        var openings = string.Empty;

        // var scheduler = new ScheduleGenerator.ScheduleGenerator(
        //     classrooms,
        //     daysOfWeek,
        //     hours,
        //     courseGroups,
        //     flyingEquipments
        // )
    }
}