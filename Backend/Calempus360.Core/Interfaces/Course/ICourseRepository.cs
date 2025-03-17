using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.Course
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Models.Course>> GetAllCoursesAsync();
        Task<Models.Course?> GetCourseByIdAsync(Guid id);
        Task<Models.Course> AddCourseAsync(Models.Course course, Guid academicYear, Guid universityId, List<Guid> equipmentType);
        Task<Models.Course> UpdateCourseAsync(Models.Course course, Guid academicYear, List<Guid> equipmentType, Guid universityId);
        Task<bool> DeleteCourseAsync(Guid id);

    }
}
