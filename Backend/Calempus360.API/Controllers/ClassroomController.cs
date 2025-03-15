using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("/api/classrooms")]
    [ApiController]
    public class ClassroomController(IClassroomService classroomService) : ControllerBase
    {
        #region GET

        [HttpGet]
        public async Task<IActionResult> GetClassrooms([FromQuery] Guid? universityId, [FromQuery] Guid? siteId)
        {
            var classrooms = await classroomService.GetClassroomsAsync(universityId, siteId);
            return Ok(classrooms.Select(c => c.MapToDto()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetClassroomById(Guid id)
        {
            var classroom = await classroomService.GetClassroomByIdAsync(id);
            return Ok(classroom.MapToDto());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> CreateClassroom(Guid siteId, [FromBody] ClassroomRequestDto requestDto)
        {
            var classroom = await classroomService.CreateClassroomAsync(new Classroom
            (
                name: requestDto.Name,
                code: requestDto.Code,
                capacity: requestDto.Capacity
            ), siteId);

            return Ok(classroom.MapToDto());
        }

        [HttpPost("{id:guid}/equipments/add/{equipmentId:guid}")]
        public async Task<IActionResult> AddEquipmentToClassroom(Guid id, Guid equipmentId, [FromQuery] Guid academicYearId)
        {
            var result = await classroomService.AddEquipmentToClassroomAsync(id, equipmentId, academicYearId);
            return Ok(result);
        }

        #endregion

        #region PUT

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateClassroom(Guid id, [FromBody] ClassroomRequestDto requestDto)
        {
            var classroom = await classroomService.UpdateClassroomAsync(new Classroom
            (
                name: requestDto.Name,
                code: requestDto.Code,
                capacity: requestDto.Capacity,
                id: id
            ));

            return Ok(classroom.MapToDto());
        }

        #endregion

        #region DELETE

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteClassroom(Guid id)
        {
            var result = await classroomService.DeleteClassroomAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id:guid}/equipments/remove/{equipmentId:guid}")]
        public async Task<IActionResult> RemoveEquipmentFromClassroom(Guid id, Guid equipmentId, [FromQuery] Guid academicYearId)
        {
            var result = await classroomService.RemoveEquipmentFromClassroomAsync(id, equipmentId, academicYearId);
            return Ok(result);
        }

        #endregion
    }
}
