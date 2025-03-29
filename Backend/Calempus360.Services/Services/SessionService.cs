using Calempus360.Core.Interfaces.Session;
using Calempus360.Core.Models;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Repositories;
using Calempus360.Services.Adapters.ScheduleGenerator;
using Microsoft.EntityFrameworkCore;
using ScheduleGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Services.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly Calempus360DbContext _context;

        public SessionService(ISessionRepository sessionRepository, Calempus360DbContext context)
        {
            _sessionRepository = sessionRepository;
            _context = context;
        }

        public async Task<Session> AddSessionAsync(Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups)
        {
            if (!CheckBusinessRules(session, classRoomId, courseId, equipments, studentGroups))
                throw new Exception("Session constraints not respected !");

            return await _sessionRepository.AddSessionAsync(session, classRoomId, courseId, equipments, studentGroups);
        }

        public async Task<bool> DeleteSessionAsync(Guid id)
        {
            return await _sessionRepository.DeleteSessionAsync(id);
        }

        public async Task<IEnumerable<Session>> GetAllSessionAsync(
            Guid? courseId,
            Guid? studentGroupId,
            Guid? classroomId,
            Guid academicYearId,
            Guid universityId)
        {
            return await _sessionRepository.GetAllSessionAsync(courseId, studentGroupId, classroomId, academicYearId, universityId);
        }

        public async Task<Session> GetSessionByIdAsync(Guid id)
        {
            return await _sessionRepository.GetSessionByIdAsync(id);
        }

        public Task<Session> UpdateSessionAsync(Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups)
        {
            // recuperer la session comme elle est en db
            var sessionInDb = _context.Sessions.FirstOrDefault(s => s.SessionId == session.Id);
            if (sessionInDb == null) throw new NotFoundException("Session not found !");

            // si on souhaite modifier la session dans le passé ou moins d'un jour avant, on ne peut pas modifier
            if (sessionInDb.DatetimeStart > DateTime.Now.AddDays(+1))
                throw new Exception("Cannot update session in the past or less than a day before !");

            if (!CheckBusinessRules(session, classRoomId, courseId, equipments, studentGroups))
                throw new Exception("Session constraints not respected !");

            return _sessionRepository.UpdateSessionAsync(session, classRoomId, courseId, equipments, studentGroups);
        }

        public async Task<bool> GenerateSchedule(Guid universityId, Guid academicYearId)
        {
            // Clean up current _context for year and university
            var academicYear = await _context.AcademicYears
                .FirstOrDefaultAsync(ay => ay.AcademicYearId == academicYearId);

            var x = from s in await _context.Sessions
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
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();

            var classrooms = from c in await _context.Classrooms
                                    .Include(c => c.ClassroomEquipments)!
                                        .ThenInclude(ce => ce.EquipmentEntity)
                                            .ThenInclude(e => e.EquipmentTypeEntity)
                                    .Include(c => c.SiteEntity)
                                    .Where(c => c.SiteEntity!.UniversityId == universityId)
                                    .ToListAsync()
                             select ClassAdapter.Adapt(c);

            var flyingEquipments = from e in await _context.Equipments
                                        .Include(e => e.EquipmentTypeEntity)
                                        .Include(e => e.UniversitySiteEquipmentEntity)
                                        .Where(e => e.UniversitySiteEquipmentEntity.UniversityId == universityId
                                                    &&
                                                    !_context.ClassroomsEquipments.Any(
                                                        ce => ce.EquipmentId == e.EquipmentId &&
                                                        ce.AcademicYearId == academicYearId))
                                        .ToListAsync()
                                   select EquipmentAdapter.Adapt(e);

            // partir depuis les groups puis option puis cours 
            // pour chaque groupe, on récupère les cours et on les ajoute à la liste des cours
            // ensuite on crée les groupes de cours
            var groups = await _context.StudentGroups
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
                var course = courseGroup.Key;
                var groupsList = courseGroup.Value.Cast<StudentGroupEntity>().ToList();
                // Supposons que la propriété qui indique le nombre d'heures est "WeeklyHours" (ou "WeeklyHour")
                var repetitions = course.WeeklyHours; // adapter le nom de la propriété si nécessaire

                for (int i = 0; i < repetitions; i++)
                {
                    courseGroups.Add(CourseGroupsAdapter.Adapt(course, groupsList));
                }
            }

            var sitesCoursesSchedules = await _context.SitesCoursesSchedules
                                        .Include(scs => scs.CourseScheduleEntity)
                                        .Include(scs => scs.SiteEntity) // Ajout de l'inclusion de SiteEntity
                                        .Where(scs => scs.SiteEntity!.UniversityId == universityId)
                                        .ToListAsync();

            var openings = sitesCoursesSchedules.SelectMany(o =>
                Enumerable.Range(
                    TimeOnlyToInt(o.CourseScheduleEntity.HourStart),
                    TimeOnlyToInt(o.CourseScheduleEntity.HourEnd) - TimeOnlyToInt(o.CourseScheduleEntity.HourStart)
                )
                .Select(hour => (
                    site: o.SiteEntity.SiteId.ToString(),
                    dayOfWeek: ((Calempus360.Core.Models.DayOfWeek)o.CourseScheduleEntity.DayOfTheWeek).ToString(),
                    startEndHour: (hour, hour + 1)
                ))
            ).ToList();

            var classroomList = classrooms.ToList();
            var courseGroupList = courseGroups.ToList();
            var flyingEquipmentList = flyingEquipments.ToList();
            var openingList = openings.ToList();

            var scheduler = new ScheduleGenerator.ScheduleGenerator(
                classroomList,
                openingList!,
                courseGroupList,
                flyingEquipmentList
            );

            var sessions = scheduler.GenerateSchedule();

            foreach (var session in sessions)
            {
                var academicYearDates =
                    Enumerable.Range(0, (academicYear!.DateEnd.ToDateTime(TimeOnly.MinValue) - academicYear.DateStart.ToDateTime(TimeOnly.MinValue)).Days + 1)
                    .Select(offset => academicYear.DateStart.AddDays(offset))
                    .Where(date => date.DayOfWeek.ToString() == session.Key.Day)
                    .ToList();

                var c = _context.Courses
                    .Include(c => c.EquipmentTypes)
                        .ThenInclude(et => et.EquipmentTypeEntity)
                    .First(c => c.CourseId == Guid.Parse(session.Value.Course));

                foreach (var date in academicYearDates)
                {
                    var id = Guid.NewGuid();
                    var name = c.Name + " - " + date.ToShortDateString() + " - " + session.Key.Day + " - " + session.Key.TimeSlot.StartHour + "h" + session.Key.TimeSlot.EndHour + "h";
                    var startHour = date.ToDateTime(TimeOnly.MinValue).AddHours(session.Key.TimeSlot.StartHour);
                    var endHour = date.ToDateTime(TimeOnly.MinValue).AddHours(session.Key.TimeSlot.EndHour);
                    var classroomId = Guid.Parse(session.Key.Location.Classroom);
                    var courseId = Guid.Parse(session.Value.Course);

                    // Save sessions
                    var sessionEntity = new SessionEntity
                    {
                        SessionId = id,
                        Name = name,
                        DatetimeStart = startHour,
                        DatetimeEnd = endHour,
                        ClassroomId = classroomId,
                        CourseId = courseId
                    };

                    _context.Sessions.Add(sessionEntity);

                    // Save groups sessions
                    foreach (var group in session.Value.Groups)
                    {
                        var groupSessionEntity = new StudentGroupSessionEntity
                        {
                            SessionId = id,
                            StudentGroupId = Guid.Parse(group)
                        };

                        _context.StudentGroupSessions.Add(groupSessionEntity);
                    }

                    // Save equipment sessions
                    foreach (var equipment in session.Value.FlyingEquipments)
                    {
                        var equipmentSessionEntity = new EquipmentSessionEntity
                        {
                            SessionId = id,
                            EquipmentId = equipment.Code.Value
                        };

                        _context.EquipmentSessions.Add(equipmentSessionEntity);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        private static int TimeOnlyToInt(TimeOnly hour)
        {
            return hour.Hour;
        }

        private bool CheckBusinessRules(Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups)
        {
            var academicYear = _context.AcademicYears
                    .FirstOrDefault(ay => ay.DateStart <= DateOnly.FromDateTime(session.DateTimeStart) && ay.DateEnd >= DateOnly.FromDateTime(session.DateTimeEnd));
            if (academicYear == null) throw new NotFoundException("Academic year not found !");

            var classRoom = _context.Classrooms
                .Include(c => c.SiteEntity)
                .FirstOrDefault(c => c.ClassroomId == classRoomId);
            if (classRoom == null)
                throw new NotFoundException("Classroom not found !");

            // vérifier si le cours est déjà planifié 2h dans la journée
            var sessions = _context.Sessions.Where(s => s.CourseId == courseId && s.DatetimeStart.Date == session.DateTimeStart.Date && s.SessionId != session.Id).ToList();
            if (sessions.Count >= 2)
                throw new Exception("Course already planned 2 times in the day !");
                
            // vérifier si la salle est déjà occupée à cette heure
            var sessionInClassroom = _context.Sessions
                .FirstOrDefault(s => s.ClassroomId == classRoomId && s.DatetimeStart == session.DateTimeStart);
            if (sessionInClassroom != null) throw new Exception("Classroom already occupied at this time !");

            // vérifier si la salle est équipée ou si les équipements volants sont disponibles à cette heure
            var classroomsEquipments = _context.ClassroomsEquipments
                .Include(ce => ce.EquipmentEntity)
                .Where(ce => ce.AcademicYearId == academicYear!.AcademicYearId)
                .Select(ce => ce.EquipmentEntity)
                .ToList(); // equipements de toutes les salles

            var classroomEquipment = from ce in classroomsEquipments where ce.ClassroomEquipments!.Any(ce => ce.ClassroomId == classRoomId) select ce;

            var flyingEquipments = _context.Equipments
                .Include(e => e.EquipmentTypeEntity)
                .Include(e => e.EquipmentSessions)
                    .ThenInclude(es => es.SessionEntity)
                .Include(e => e.UniversitySiteEquipmentEntity)
                .Where(
                    e => e.UniversitySiteEquipmentEntity.SiteId == classRoom.SiteEntity!.SiteId
                    && !classroomsEquipments.Contains(e)
                    && !e.EquipmentSessions.Any(es => es.SessionEntity.DatetimeStart == session.DateTimeStart)
                )
                .ToList(); // equipement volants disponibles pour cette heure sur le site

            equipments.ForEach(id =>
            {
                var equipment = _context.Equipments
                    .Include(e => e.EquipmentTypeEntity)
                    .Include(e => e.UniversitySiteEquipmentEntity)
                    .Where(e => e.UniversitySiteEquipmentEntity.SiteId == classRoom.SiteEntity!.SiteId)
                    .FirstOrDefault(e => e.EquipmentId == id);

                if (equipment == null) throw new NotFoundException("Equipment not found !");
                if (!classroomEquipment.Contains(equipment) && !flyingEquipments.Contains(equipment))
                    throw new Exception("Equipment not available in this classroom !");
            });

            // vérifier si les groupes sont disponibles à cette heure
            studentGroups.ForEach(id =>
            {
                var group = _context.StudentGroups
                    .Include(g => g.StudentGroupSessions)
                        .ThenInclude(sgs => sgs.SessionEntity)
                    .FirstOrDefault(g => g.StudentGroupId == id);

                if (group == null) throw new NotFoundException("Student group not found !");
                if (group.StudentGroupSessions.Any(sgs => sgs.SessionEntity.DatetimeStart == session.DateTimeStart && sgs.SessionId != session.Id))
                    throw new Exception("Student group not available at this time !");
                    

                // verifier si la session d'avant est sur le même site
                var sessionBefore = _context.Sessions
                    .Include(s => s.ClassroomEntity)
                    .FirstOrDefault(s => s.DatetimeEnd == session.DateTimeStart && s.ClassroomEntity.SiteId != classRoom.SiteEntity!.SiteId && s.StudentGroupSessions.Any(sg => sg.StudentGroupId == group.StudentGroupId));
                if (sessionBefore != null)
                    throw new Exception("Student group not available at this time !");

                // verfiier si la session d'après est sur le même site
                var sessionAfter = _context.Sessions
                    .Include(s => s.ClassroomEntity)
                    .FirstOrDefault(s => s.DatetimeStart == session.DateTimeEnd && s.ClassroomEntity.SiteId != classRoom.SiteEntity!.SiteId && s.StudentGroupSessions.Any(sg => sg.StudentGroupId == group.StudentGroupId));
                if (sessionAfter != null)
                    throw new Exception("Student group not available at this time !");
                    
            });

            // vérifier si les groupes suivent ce cours
            studentGroups.ForEach(id =>
            {
                var group = _context.StudentGroups
                    .Include(g => g.OptionEntity)
                        .ThenInclude(o => o!.OptionCourses)
                            .ThenInclude(oc => oc.CourseEntity)
                    .FirstOrDefault(g => g.StudentGroupId == id);

                if (group == null) throw new NotFoundException("Student group not found !");
                if (!group.OptionEntity!.OptionCourses.Any(oc => oc.CourseId == courseId))
                    throw new Exception("Student group does not follow this course !");
            });

            return true;
        }
    }
}
