using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.Interfaces.Schedule;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController: ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        
        public StudentController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        
        // GET: api/students/{groupId}/schedule
        [HttpGet("{groupId}/schedule")]
        public async Task<IActionResult> GetGroupSchedule([FromQuery] GetGroupScheduleRequest request)
        {
            var response = await _scheduleService.GetGroupScheduleAsync(request);
            return Ok(response);
        }
    }
}
