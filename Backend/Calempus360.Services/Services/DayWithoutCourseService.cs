using Calempus360.Core.Interfaces.DayWithoutCourse;
using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Services.Services
{
    public class DayWithoutCourseService : IDayWithoutCourseService
    {
        private readonly IDayWithoutCourseRepository _dayWithoutCourseRepository;

        public DayWithoutCourseService(IDayWithoutCourseRepository dayWithoutCourseRepository)
        {
            _dayWithoutCourseRepository = dayWithoutCourseRepository;
        }

        public async Task<DayWithoutCourse> AddDayWithoutCourseAsync(DayWithoutCourse dayWithoutCourse, Guid academicYear)
        {
            return await _dayWithoutCourseRepository.AddDayWithoutCourseAsync(dayWithoutCourse, academicYear);
        }

        public async Task<bool> DeleteDayWithoutCourseAsync(Guid id)
        {
            return await _dayWithoutCourseRepository.DeleteDayWithoutCourseAsync(id);
        }

        public async Task<IEnumerable<DayWithoutCourse>> GetAllDayWithoutCourseAsync(Guid academicYear)
        {
            return await _dayWithoutCourseRepository.GetAllDayWithoutCourseAsync(academicYear);
        }

        public async Task<DayWithoutCourse> GetDayWithoutCourseByIdAsync(Guid id)
        {
            return await _dayWithoutCourseRepository.GetDayWithoutCourseByIdAsync(id);
        }

        public async Task<DayWithoutCourse> UpdateDayWithoutCourseAsync(DayWithoutCourse dayWithoutCourse)
        {
            return await _dayWithoutCourseRepository.UpdateDayWithoutCourseAsync(dayWithoutCourse);
        }
    }
}
