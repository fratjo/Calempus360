﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.Session
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Models.Session>> GetAllSessionAsync(
            Guid? courseId,
            Guid? studentGroupId,
            Guid? classroomId,
            Guid academicYearId,
            Guid universityId);
        Task<Models.Session> GetSessionByIdAsync(Guid id);
        Task<Models.Session> AddSessionAsync(Models.Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups);
        Task<Models.Session> UpdateSessionAsync(Models.Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups);
        Task<bool> DeleteSessionAsync(Guid id);
    }
}
