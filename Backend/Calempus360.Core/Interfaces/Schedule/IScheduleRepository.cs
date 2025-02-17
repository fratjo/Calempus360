using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.Schedule;

public interface IScheduleRepository
{
    Task<Session> GetScheduleByGroupIdAsync(int groupId);
}