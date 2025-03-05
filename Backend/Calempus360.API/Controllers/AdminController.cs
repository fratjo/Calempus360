using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.Interfaces.Group;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IStudentGroupService _studentGroupService;

        public AdminController(IStudentGroupService studentGroupService)
        {
            _studentGroupService = studentGroupService;
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetAllStudentGroups(string academicYear)
        {
            var studentGroups = await _studentGroupService.GetAllStudentGroupAsync(academicYear);

            return Ok(studentGroups);
        }

        [HttpGet("group/{id:guid}")]
        public async Task<IActionResult> GetStudentGroupById(Guid id, string academicYear)
        {
            var studentGroup = await _studentGroupService.GetStudentGroupByIdAsync(id, academicYear);
            return Ok(studentGroup);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentGroup(GetStudentGroupRequest studentGroupRequest, string academicYear)
        {
            await _studentGroupService.AddStudentGroupAsync(studentGroupRequest, academicYear);
            return Ok($"Student Group {studentGroupRequest.Code} added !");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentGroup(GetStudentGroupRequest studentGroupRequest, Guid id)
        {
            var response = await _studentGroupService.UpdateStudentGroupAsync(studentGroupRequest, id);
            return Ok($"Student Group {studentGroupRequest.Code} updated !");
        }

        [HttpDelete("group/{id:guid}")]
        public async Task<IActionResult> DeleteStudentGroup(Guid id)
        {
            var response = await _studentGroupService.DeleteStudentGroupByIdAsync(id);
            if (!response) return NotFound("No Student Group was deleted !");
            return Ok(response);
        }



    }
}
