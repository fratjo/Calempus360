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
        Task<IEnumerable<GetStudentGroupResponse>> GetAllStudentGroupAsync();
        Task<GetStudentGroupResponse> GetStudentGroupByIdAsync(Guid id);
        Task AddStudentGroupAsync(GetStudentGroupRequest studentGroup);
        Task<bool> UpdateStudentGroupAsync(GetStudentGroupRequest studentGroup, Guid id);
        Task<bool> DeleteStudentGroupByIdAsync(Guid id);
    }
}
