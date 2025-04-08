using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.Schedule
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Models.Session>> GetAllScheduleAsync();
        Task<Models.Session> GetScheduleByIdAsync(Guid id);
        Task<Models.Session> AddScheduleAsync(Models.Session schedule);
        Task<Models.Session> UpdateScheduleAsync(Models.Session schedule);
        Task<bool> DeleteScheduleAsync(Guid id);
    }
}
