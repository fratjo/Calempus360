using Calempus360.Core.Interfaces.Session;
using Calempus360.Core.Models;
using Calempus360.Infrastructure.Repositories;
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

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<Session> AddSessionAsync(Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups)
        {
            return await _sessionRepository.AddSessionAsync(session, classRoomId, courseId, equipments, studentGroups);
        }

        public async Task<bool> DeleteSessionAsync(Guid id)
        {
            return await _sessionRepository.DeleteSessionAsync(id);
        }

        public async Task<IEnumerable<Session>> GetAllSessionAsync()
        {
            return await _sessionRepository.GetAllSessionAsync();
        }

        public async Task<Session> GetSessionByIdAsync(Guid id)
        {
            return await _sessionRepository.GetSessionByIdAsync(id);
        }

        public Task<Session> UpdateSessionAsync(Session session, Guid classRoomId, Guid courseId, List<Guid> equipments, List<Guid> studentGroups)
        {
            return _sessionRepository.UpdateSessionAsync(session, classRoomId, courseId, equipments, studentGroups);
        }
    }
}
