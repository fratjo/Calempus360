using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.Group
{
    public interface IStudentGroupRepository
    {
        Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync(Guid academicYear, Guid universityId);
        Task<StudentGroup> GetStudentGroupByIdAsync(Guid id, Guid academicYear);
        Task<StudentGroup> AddStudentGroupAsync(StudentGroup studentGroup, Guid academicYear, Guid Option, Guid Site);
        Task<StudentGroup> UpdateStudentGroupAsync(StudentGroup studentGroup, Guid option, Guid site);
        Task<bool> DeleteStudentGroupByIdAsync(Guid id);
    }
}
