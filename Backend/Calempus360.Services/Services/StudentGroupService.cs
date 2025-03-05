using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Group;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Errors.StudentGroup;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Services.Services
{
    public class StudentGroupService : IStudentGroupService
    {
        private readonly IStudentGroupRepository _studentGroupRepository;

        public StudentGroupService(IStudentGroupRepository studentGroupRepository)
        {
            _studentGroupRepository = studentGroupRepository;
        }

        public async Task<StudentGroup> AddStudentGroupAsync(StudentGroup studentGroup, Guid academicYear, Guid option, Guid site)
        {
            return await _studentGroupRepository.AddStudentGroupAsync(studentGroup, academicYear,option,site);
            
        }

        public async Task<bool> DeleteStudentGroupByIdAsync(Guid id)
        {
            var isDeleted = await _studentGroupRepository.DeleteStudentGroupByIdAsync(id);
            return isDeleted;
        }

        public async Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync(Guid academicYear)
        {
            return await _studentGroupRepository.GetAllStudentGroupAsync(academicYear);
        }

        public async Task<StudentGroup> GetStudentGroupByIdAsync(Guid id, Guid academicYear)
        {
            return await _studentGroupRepository.GetStudentGroupByIdAsync(id, academicYear);
        }

        public async Task<StudentGroup> UpdateStudentGroupAsync(StudentGroup studentGroup, Guid option, Guid site)
        { 
            return await _studentGroupRepository.UpdateStudentGroupAsync(studentGroup,option,site);
        }




    }
}
