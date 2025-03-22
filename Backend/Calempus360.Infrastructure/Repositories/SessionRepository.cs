using Calempus360.Core.Interfaces.Schedule;
using Calempus360.Core.Interfaces.Session;
using Calempus360.Core.Models;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly Calempus360DbContext _context;

        public SessionRepository (Calempus360DbContext context)
        {
            _context = context;
        }

        public async Task<Session> AddSessionAsync(Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups)
        {
            var entity = session.ToEntity();

            var classRoomEntity = await _context.Classrooms.FindAsync(classRoomId);
            if (classRoomEntity == null) throw new NotFoundException("Classroom not found !");
            entity.ClassroomEntity = classRoomEntity;

            var courseEntity = await _context.Courses.FindAsync(courseId);
            if (courseEntity == null) throw new NotFoundException("Course not found !");
            entity.CourseEntity = courseEntity;

            foreach (var equipmentId in equipments)
            {
                var equipment = await _context.Equipments.FindAsync(equipmentId);
                if(equipment == null) throw new NotFoundException("Equipment not found !");
                var equipmentSession = new Persistence.Entities.EquipmentSessionEntity
                {
                    EquipmentId = equipmentId,
                    SessionId = session.Id,
                };

                _context.EquipmentSessions.Add(equipmentSession);
            }

            foreach (var studentGroupId in studentGroups)
            {
                var studentGroup = await _context.StudentGroups.FindAsync(studentGroupId);
                if (studentGroup == null) throw new NotFoundException("Student Group not found !");
                var studentGroupSessionEntity = new Persistence.Entities.StudentGroupSessionEntity
                {
                    StudentGroupId = studentGroupId,
                    SessionId = session.Id,
                };
                _context.StudentGroupSessions.Add(studentGroupSessionEntity);
            }

            await _context.Sessions.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ToDomainModel();

        }

        public async Task<bool> DeleteSessionAsync(Guid id)
        {
            var entity = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionId == id);

            if (entity == null) throw new NotFoundException("Session not found");

            _context.Sessions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Session>> GetAllSessionAsync()
        {
            var entities = await _context.Sessions
                            .Include(c => c.ClassroomEntity)
                            .Include(cs => cs.CourseEntity)
                            .Include(gs => gs.StudentGroupSessions)
                                .ThenInclude(sgs => sgs.StudentGroupEntity)
                            .Include(es => es.EquipmentSessions)
                                .ThenInclude(sgs => sgs.EquipmentEntity)
                            .ToListAsync();
            return entities.Select(e => e.ToDomainModel()).ToList();
        }

        public async Task<Session> GetSessionByIdAsync(Guid id)
        {
            var entity = await _context.Sessions
                            .Include(c => c.ClassroomEntity)
                            .Include(cs => cs.CourseEntity)
                            .Include(gs => gs.StudentGroupSessions)
                                .ThenInclude(sgs => sgs.StudentGroupEntity)
                            .Include(es => es.EquipmentSessions)
                                .ThenInclude(sgs => sgs.EquipmentEntity)
                            .FirstOrDefaultAsync(s => s.SessionId == id);
            if (entity == null) throw new NotFoundException("Session not found !");
            return entity.ToDomainModel();
        }

        public async Task<Session> UpdateSessionAsync(Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups)
        {
            var entity = await _context.Sessions
                            .Include(c => c.ClassroomEntity)
                            .Include(cs => cs.CourseEntity)
                            .Include(gs => gs.StudentGroupSessions)
                                .ThenInclude(sgs => sgs.StudentGroupEntity)
                            .Include(es => es.EquipmentSessions)
                                .ThenInclude(sgs => sgs.EquipmentEntity)
                            .FirstOrDefaultAsync(s => s.SessionId == session.Id);
            if (entity == null) throw new NotFoundException("Session not found !");

            var classRoomEntity = await _context.Classrooms.FindAsync(classRoomId);
            if (classRoomEntity == null) throw new NotFoundException("Classroom not found !");
            entity.ClassroomEntity = classRoomEntity;

            var courseEntity = await _context.Courses.FindAsync(courseId);
            if (courseEntity == null) throw new NotFoundException("Course not found !");
            entity.CourseEntity = courseEntity;

            entity.Name = session.Name;
            entity.DatetimeStart = session.DateTimeStart;
            entity.DatetimeEnd = session.DateTimeEnd;
            entity.UpdatedAt = DateTime.Now;

            entity.EquipmentSessions.RemoveAll(es => !equipments.Contains(es.EquipmentEntity.EquipmentId));

            foreach (var equipmentId in equipments)
            {
                var equipment = await _context.Equipments.FindAsync(equipmentId);
                if (equipment == null) throw new NotFoundException("Equipment not found !");
                if (!entity.EquipmentSessions.ToList().Any(es => es.EquipmentId == equipmentId)) 
                {
                    entity.EquipmentSessions.Add(new Persistence.Entities.EquipmentSessionEntity
                    {
                        EquipmentEntity = equipment,
                        SessionEntity = entity,
                    });
                }    
                
            }

            entity.StudentGroupSessions.RemoveAll(sgs => !studentGroups.Contains(sgs.StudentGroupEntity.StudentGroupId));

            foreach (var studentGroupId in studentGroups)
            {
                var studentGroup = await _context.StudentGroups.FindAsync(studentGroupId);
                if (studentGroup == null) throw new NotFoundException("Student Group not found !");
                if (!entity.StudentGroupSessions.ToList().Any(es => es.StudentGroupId == studentGroupId))
                {
                    entity.StudentGroupSessions.Add(new Persistence.Entities.StudentGroupSessionEntity
                    {
                        StudentGroupEntity = studentGroup,
                        SessionEntity = entity,
                    });
                }            
            }

            await _context.SaveChangesAsync();
            return entity.ToDomainModel();

        }
    }
}
