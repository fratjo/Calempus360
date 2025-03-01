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
        Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync();
        Task<StudentGroup?> GetStudentGroupByIdAsync(Guid id);
        Task AddStudentGroupAsync(StudentGroup studentGroup);
        Task<bool> UpdateStudentGroupAsync(StudentGroup studentGroup);
        Task<bool> DeleteStudentGroupByIdAsync(Guid id);
    }
}
