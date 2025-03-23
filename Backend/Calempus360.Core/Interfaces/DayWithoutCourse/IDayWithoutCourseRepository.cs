using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.DayWithoutCourse
{
    public interface IDayWithoutCourseRepository
    {
        Task<IEnumerable<Models.DayWithoutCourse>> GetAllDayWithoutCourseAsync(Guid academicYear);
        Task<Models.DayWithoutCourse> GetDayWithoutCourseByIdAsync(Guid id);
        Task<Models.DayWithoutCourse> AddDayWithoutCourseAsync(Models.DayWithoutCourse dayWithoutCourse, Guid academicYear);
        Task<Models.DayWithoutCourse> UpdateDayWithoutCourseAsync(Models.DayWithoutCourse dayWithoutCourse);
        Task<bool> DeleteDayWithoutCourseAsync(Guid id);

    }
}
