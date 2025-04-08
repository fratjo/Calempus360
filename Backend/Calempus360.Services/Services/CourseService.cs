using Calempus360.Core.Interfaces.Course;
using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService (ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> AddCourseAsync(Course course, Guid academicYear, Guid universityId, List<Guid> equipmentType)
        {
            return await _courseRepository.AddCourseAsync(course,academicYear, universityId, equipmentType);
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            return await _courseRepository.DeleteCourseAsync(id);
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await _courseRepository.GetCourseByIdAsync(id);
        }

        public async Task<Course> UpdateCourseAsync(Course course, Guid academicYear, List<Guid> equipmentType, Guid universityId)
        {
            return await _courseRepository.UpdateCourseAsync(course, academicYear, equipmentType, universityId);
        }
    }
}
