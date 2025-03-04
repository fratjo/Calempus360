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

namespace Calempus360.Services.StudentGroupService
{
    public class StudentGroupService : IStudentGroupService
    {
        private readonly IStudentGroupRepository _studentGroupRepository;

        public StudentGroupService(IStudentGroupRepository studentGroupRepository)
        {
            _studentGroupRepository = studentGroupRepository;
        }

        public async Task AddStudentGroupAsync(AddStudentGroupRequest studentGroupRequest, string AcademicYear)
        {
            if (studentGroupRequest.NumberOfStudents < 20 || studentGroupRequest.NumberOfStudents > 40)
            {
                throw new StudentGroupSizeException();
            }

            if(studentGroupRequest.Code.IsNullOrEmpty() || studentGroupRequest.OptionGrade <= 0 
                || studentGroupRequest.Option.IsNullOrEmpty() || studentGroupRequest.Site.IsNullOrEmpty())
            {
                throw new InvalidStudentGroupException();
            }

            //Pour Tester
            var site = await _studentGroupRepository.GetSiteByName(studentGroupRequest.Site);
            var option = await _studentGroupRepository.GetOptionByName(studentGroupRequest.Option);

            var studentGroup = new StudentGroup(Guid.NewGuid(),studentGroupRequest.Code, studentGroupRequest.NumberOfStudents,
                studentGroupRequest.OptionGrade, DateTime.Now, DateTime.Now, site, option);

            await _studentGroupRepository.AddStudentGroupAsync(studentGroup, AcademicYear);
        }

        public async Task<bool> DeleteStudentGroupByIdAsync(Guid id)
        {
            var isDeleted = await _studentGroupRepository.DeleteStudentGroupByIdAsync(id);
            if (!isDeleted) throw new StudentGroupNotFoundException(id);
            return isDeleted;
        }

        public async Task<IEnumerable<GetStudentGroupResponse>> GetAllStudentGroupAsync(string academicYear)
        {
            var studentGroupsResponse = new List<GetStudentGroupResponse>();
            var studentGroups = await _studentGroupRepository.GetAllStudentGroupAsync(academicYear);

            foreach (var group in studentGroups)
            {
                studentGroupsResponse.Add(new GetStudentGroupResponse()
                {
                    Code = group.Code,
                    OptionGrade = group.OptionGrade,
                    Option = group.Option.Name,
                    NumberOfStudents = group.NumberOfStudents,
                    Site = group.Site.Name
                });
            }

            return studentGroupsResponse;
        }

        public async Task<GetStudentGroupResponse> GetStudentGroupByIdAsync(Guid id, string academicYear)
        {
            var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(id,academicYear);
            if (studentGroup == null) throw new StudentGroupNotFoundException(id);
            return new GetStudentGroupResponse()
            {
                Code = studentGroup.Code,
                OptionGrade = studentGroup.OptionGrade,
                Option = studentGroup.Option.Name,
                NumberOfStudents = studentGroup.NumberOfStudents,
                Site = studentGroup.Site.Name
            };
        }

        public async Task<bool> UpdateStudentGroupAsync(UpdateStudentGroupRequest studentGroupRequest)
        {
            if (studentGroupRequest.NumberOfStudents < 20 || studentGroupRequest.NumberOfStudents > 40)
            {
                throw new StudentGroupSizeException();
            }

            if (studentGroupRequest.Code.IsNullOrEmpty() || studentGroupRequest.OptionGrade <= 0
                || studentGroupRequest.Option.IsNullOrEmpty() || studentGroupRequest.Site.IsNullOrEmpty())
            {
                throw new InvalidStudentGroupException();
            }

            //Pour Tester
            var site = await _studentGroupRepository.GetSiteByName(studentGroupRequest.Site);
            var option = await _studentGroupRepository.GetOptionByName(studentGroupRequest.Option);
            
            var studentGroupUpdated = new StudentGroup(studentGroupRequest.Id, studentGroupRequest.Code, studentGroupRequest.NumberOfStudents,
                studentGroupRequest.OptionGrade, DateTime.Now, DateTime.Now, site, option);//Remplacer qd j'ai service Site et Option

            var isUpdated = await _studentGroupRepository.UpdateStudentGroupAsync(studentGroupUpdated,studentGroupRequest.Id);
            if (!isUpdated) throw new StudentGroupNotFoundException(studentGroupRequest.Id);
  
            return isUpdated;
        }

        


    }
}
