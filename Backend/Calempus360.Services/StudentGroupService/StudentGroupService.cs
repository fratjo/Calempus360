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
        //TODO Repo Site et Option pour completer le service
        public StudentGroupService(IStudentGroupRepository studentGroupRepository)
        {
            _studentGroupRepository = studentGroupRepository;
        }

        public async Task AddStudentGroupAsync(GetStudentGroupRequest studentGroupRequest)
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
            //var option

            var studentGroup = new StudentGroup(Guid.NewGuid(),studentGroupRequest.Code, studentGroupRequest.NumberOfStudents,
                studentGroupRequest.OptionGrade, DateTime.Now, DateTime.Now, null, null);

            await _studentGroupRepository.AddStudentGroupAsync(studentGroup);
        }

        public async Task<bool> DeleteStudentGroupByIdAsync(Guid id)
        {
            return await _studentGroupRepository.DeleteStudentGroupByIdAsync(id);
        }

        public async Task<IEnumerable<GetStudentGroupResponse>> GetAllStudentGroupAsync()
        {
            var studentGroupsResponse = new List<GetStudentGroupResponse>();
            var studentGroups = await _studentGroupRepository.GetAllStudentGroupAsync();

            foreach (var group in studentGroups)
            {
                studentGroupsResponse.Add(new GetStudentGroupResponse()
                {
                    Code = group.Code,
                    OptionGrade = group.OptionGrade,
                    Option = group.Option.ToString()!,
                    NumberOfStudents = group.NumberOfStudents,
                    Site = group.Site.ToString()!
                });
            }

            return studentGroupsResponse;
        }

        public async Task<GetStudentGroupResponse> GetStudentGroupByIdAsync(Guid id)
        {
            var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(id);
            if (studentGroup == null) throw new StudentGroupNotFoundException(id);
            return new GetStudentGroupResponse()
            {
                Code = studentGroup.Code,
                OptionGrade = studentGroup.OptionGrade,
                Option = studentGroup.Option.ToString()!,
                NumberOfStudents = studentGroup.NumberOfStudents,
                Site = studentGroup.Site.ToString()!
            };
        }

        public async Task<bool> UpdateStudentGroupAsync(GetStudentGroupRequest studentGroupRequest, Guid id)
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

            var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(id);
            if (studentGroup == null) throw new StudentGroupNotFoundException(id);

            var studentGroupUpdated = new StudentGroup(id, studentGroupRequest.Code, studentGroupRequest.NumberOfStudents,
                studentGroupRequest.OptionGrade, studentGroup.CreatedAt, DateTime.Now,null,null);

            return await _studentGroupRepository.UpdateStudentGroupAsync(studentGroupUpdated);
        }

        


    }
}
