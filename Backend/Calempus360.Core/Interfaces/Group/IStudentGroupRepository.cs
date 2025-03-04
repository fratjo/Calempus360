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
        Task<StudentGroup> GetStudentGroupByIdAsync(Guid id, string academicYear);
        Task<StudentGroup> AddStudentGroupAsync(StudentGroup studentGroup, string academicYear);
        Task<StudentGroup> UpdateStudentGroupAsync(StudentGroup studentGroup, Guid id);
        Task<bool> DeleteStudentGroupByIdAsync(Guid id);
        //Uniquement pour Test
        Task<Models.Site> GetSiteByName(string name);
        Task<Models.Option> GetOptionByName(string name);
    }
}
