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
        Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync(string academicYear);
        Task<StudentGroup?> GetStudentGroupByIdAsync(Guid id, string academicYear);
        Task AddStudentGroupAsync(StudentGroup studentGroup, string academicYear);
        Task<bool> UpdateStudentGroupAsync(StudentGroup studentGroup);
        Task<bool> DeleteStudentGroupByIdAsync(Guid id);
        //Uniquement pour Test
        Task<Site> GetSiteByName(string name);
    }
}
