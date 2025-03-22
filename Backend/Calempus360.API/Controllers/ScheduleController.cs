using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Schedule;
using Calempus360.Core.Interfaces.Session;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    public class ScheduleController(IScheduleService scheduleService, ISessionService sessionService) : ControllerBase
    {

        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAllSessions()
        {
            var sessions = await sessionService.GetAllSessionAsync();
            return Ok(sessions.Select(s => s.MapToDto()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSessionById(Guid id)
        {
            var session = await sessionService.GetSessionByIdAsync(id);
            return Ok(session.MapToDto());
        }
        #endregion

        #region POST

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateSchedule(Guid universityId, Guid academicYearId)
        {
            return await scheduleService.GenerateSchedule(universityId, academicYearId) ? Ok() : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddSession(SessionRequestDto sessionRequest)
        {
            var session = await sessionService.AddSessionAsync(new Core.Models.Session
                (
                    name: sessionRequest.Name,
                    dateTimeStart: sessionRequest.DateTimeStart,
                    dateTimeEnd: sessionRequest.DateTimeEnd
                ), sessionRequest.ClassroomId, sessionRequest.CourseId, sessionRequest.EquipmentId, sessionRequest.StudentGroupsId);

            return Ok(session.MapToDto());
        }
        #endregion

        #region PUT
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSession(Guid id, SessionRequestDto sessionRequest)
        {
            var session = await sessionService.UpdateSessionAsync(new Core.Models.Session
                (
                    id:id,
                    name: sessionRequest.Name,
                    dateTimeStart: sessionRequest.DateTimeStart,
                    dateTimeEnd: sessionRequest.DateTimeEnd
                ), sessionRequest.ClassroomId, sessionRequest.CourseId, sessionRequest.EquipmentId, sessionRequest.StudentGroupsId);

            return Ok(session.MapToDto());
        }
        #endregion

        #region DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSessionById(Guid id)
        {
            var response = await sessionService.DeleteSessionAsync(id);
            return Ok(new { message = $"Session with id: {id} deleted" });
        }
        #endregion

    }
}