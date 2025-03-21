using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.Schedule
{
    public interface IScheduleService
    {
        Task<bool> GenerateSchedule(Guid universityId, Guid academicYearId);
    }
}