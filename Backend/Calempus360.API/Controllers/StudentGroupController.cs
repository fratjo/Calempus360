using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Group;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentGroupController : ControllerBase
    {
        private readonly IStudentGroupService _studentGroupService;

        public StudentGroupController(IStudentGroupService studentGroupService)
        {
            _studentGroupService = studentGroupService;
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetAllStudentGroups(Guid academicYear)
        {
            var studentGroups = await _studentGroupService.GetAllStudentGroupAsync(academicYear);

            return Ok(studentGroups.Select(sg => sg.MapToDto()));
        }

        [HttpGet("group/{id:guid}")]
        public async Task<IActionResult> GetStudentGroupById(Guid id, Guid academicYear)
        {
            var studentGroup = await _studentGroupService.GetStudentGroupByIdAsync(id, academicYear);
            return Ok(studentGroup.MapToDto());
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentGroup([FromBody] StudentGroupRequestDto studentGroupRequest, Guid academicYear, Guid option, Guid site)
        {
            var studentGroup = await _studentGroupService.AddStudentGroupAsync(new Core.Models.StudentGroup
                (
                    code: studentGroupRequest.Code,
                    numberOfStudents: studentGroupRequest.NumberOfStudents,
                    optionGrade: studentGroupRequest.OptionGrade
                ), academicYear,option,site);
            return Ok(studentGroup.MapToDto());
        }

        [HttpPut("group/{id:guid}")]
        public async Task<IActionResult> UpdateStudentGroup(StudentGroupRequestDto studentGroupRequest, Guid id, Guid option, Guid site)
        {
            var studentGroup = await _studentGroupService.UpdateStudentGroupAsync(new Core.Models.StudentGroup
                (
                    code: studentGroupRequest.Code,
                    numberOfStudents: studentGroupRequest.NumberOfStudents,
                    optionGrade: studentGroupRequest.OptionGrade,
                    id: id
                ),option,site);
            return Ok(studentGroup.MapToDto());
        }

        [HttpDelete("group/{id:guid}")]
        public async Task<IActionResult> DeleteStudentGroup(Guid id)
        {
            var response = await _studentGroupService.DeleteStudentGroupByIdAsync(id);
            if (!response) return NotFound("No Student Group was deleted !");
            return Ok($"Student Group with id : {id} deleted");
        }



    }
}
