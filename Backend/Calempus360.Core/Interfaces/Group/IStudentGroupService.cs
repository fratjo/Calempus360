using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Interfaces.Group
{
    public interface IStudentGroupService
    {
        Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync(string academicYear);
        Task<StudentGroup> GetStudentGroupByIdAsync(Guid id, string academicYear);
        Task<StudentGroup> AddStudentGroupAsync(StudentGroup studentGroup, string academicYear);
        Task<StudentGroup> UpdateStudentGroupAsync(StudentGroup studentGroup, Guid id);
        Task<bool> DeleteStudentGroupByIdAsync(Guid id);
    }
}
