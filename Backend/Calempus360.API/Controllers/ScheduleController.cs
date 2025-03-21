using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calempus360.Core.Interfaces.Schedule;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    public class ScheduleController(IScheduleService scheduleService) : ControllerBase
    {
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateSchedule(Guid universityId, Guid academicYearId)
        {
            return await scheduleService.GenerateSchedule(universityId, academicYearId) ? Ok() : BadRequest();
        }
    }
}