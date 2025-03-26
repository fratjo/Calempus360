using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Group;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class StudentGroupController : ControllerBase
    {
        private readonly IStudentGroupService _studentGroupService;

        public StudentGroupController(IStudentGroupService studentGroupService)
        {
            _studentGroupService = studentGroupService;
        }
        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAllStudentGroups([FromQuery] Guid academicYear, [FromQuery] Guid universityId)
        {
            var studentGroups = await _studentGroupService.GetAllStudentGroupAsync(academicYear, universityId);

            return Ok(studentGroups.Select(sg => sg.MapToDto()));
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentGroupById(Guid id, [FromQuery] Guid academicYear)
        {
            var studentGroup = await _studentGroupService.GetStudentGroupByIdAsync(id, academicYear);
            return Ok(studentGroup.MapToDto());
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<IActionResult> AddStudentGroup([FromBody] StudentGroupRequestDto studentGroupRequest, [FromQuery] Guid academicYear, [FromQuery] Guid option, [FromQuery] Guid site)
        {
            var studentGroup = await _studentGroupService.AddStudentGroupAsync(new Core.Models.StudentGroup
                (
                    code: studentGroupRequest.Code,
                    numberOfStudents: studentGroupRequest.NumberOfStudents,
                    optionGrade: studentGroupRequest.OptionGrade
                ), academicYear, option, site);
            return Ok(studentGroup.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudentGroup([FromBody] StudentGroupRequestDto studentGroupRequest, Guid id, [FromQuery] Guid option, [FromQuery] Guid site)
        {
            var studentGroup = await _studentGroupService.UpdateStudentGroupAsync(new Core.Models.StudentGroup
                (
                    code: studentGroupRequest.Code,
                    numberOfStudents: studentGroupRequest.NumberOfStudents,
                    optionGrade: studentGroupRequest.OptionGrade,
                    id: id
                ), option, site);
            return Ok(studentGroup.MapToDto());
        }

        #endregion

        #region DELETE

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudentGroup(Guid id)
        {
            var response = await _studentGroupService.DeleteStudentGroupByIdAsync(id);
            return Ok(new { message = $"Student Group with id: {id} deleted" });
        }

        #endregion

    }
}
