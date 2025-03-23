using System.Net;
using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.AcademicYear;
using Calempus360.Core.Interfaces.DayWithoutCourse;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/academic-years")]
    [ApiController]
    public class AcademicYearController(IAcademicYearService academicYearService, IDayWithoutCourseService dayWithoutCourseService) : ControllerBase
    {
        #region GET
         
        [HttpGet]
        public async Task<IActionResult> GetAcademicYears()
        {
            var academicYears = await academicYearService.GetAcademicYearsAsync();
            return Ok(academicYears.Select(ac => ac.MapToDto()));
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAcademicYearById(Guid id)
        {
            var academicYear = await academicYearService.GetAcademicYearByIdAsync(id);
            return Ok(academicYear.MapToDto());
        }

        [HttpGet("non-working-days")]
        public async Task<IActionResult> GetAllDayWithoutCourse(Guid academicYear)
        {
            var daysWithoutCourse = await dayWithoutCourseService.GetAllDayWithoutCourseAsync(academicYear);
            return Ok(daysWithoutCourse.Select(dwc => dwc.MapToDto()).ToList());
        }

        [HttpGet("non-working-days/{id:guid}")]
        public async Task<IActionResult> GetDayWithoutCourseById(Guid id)
        {
            var dayWithoutCourse = await dayWithoutCourseService.GetDayWithoutCourseByIdAsync(id);
            return Ok(dayWithoutCourse.MapToDto());
        }
        #endregion
        
        #region POST
        
        [HttpPost]
        public async Task<IActionResult> CreateAcademicYear([FromBody] AcademicYearRequestDto requestDto)
        {   
            var academicYear = new AcademicYear(
                code: requestDto.Code,
                dateStart: DateOnly.FromDateTime(requestDto.DateStart),
                dateEnd: DateOnly.FromDateTime(requestDto.DateEnd));
            
            var createdAcademicYear = await academicYearService.CreateAcademicYearAsync(academicYear);
            
            return CreatedAtAction(nameof(GetAcademicYearById), new { id = createdAcademicYear.Id }, createdAcademicYear.MapToDto());
        }

        [HttpPost("day-without-course")]
        public async Task<IActionResult> AddDayWithoutCourse([FromBody] DayWithoutCourseRequestDto dayWithoutCourseRequest, [FromQuery] Guid academicYear)
        {
            var dayWithoutCourse = await dayWithoutCourseService.AddDayWithoutCourseAsync(new DayWithoutCourse(
                    name: dayWithoutCourseRequest.Name,
                    date: DateOnly.FromDateTime(dayWithoutCourseRequest.Date)
                    ), academicYear);
            return Ok(dayWithoutCourse.MapToDto());
        }

        #endregion

        #region PUT

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAcademicYear(Guid id, [FromBody] AcademicYearRequestDto requestDto)
        {
            var academicYear = new AcademicYear(
                code: requestDto.Code,
                dateStart: DateOnly.FromDateTime(requestDto.DateStart),
                dateEnd: DateOnly.FromDateTime(requestDto.DateEnd));
            
            academicYear = await academicYearService.UpdateAcademicYearAsync(id, academicYear);
            
            return Ok(academicYear.MapToDto());
        }

        [HttpPut("non-working-days/{id:guid}")]
        public async Task<IActionResult> UpdateDayWithoutCourse(Guid id, [FromBody] DayWithoutCourseRequestDto dayWithoutCourseRequest)
        {
            var dayWithoutCourse = await dayWithoutCourseService.UpdateDayWithoutCourseAsync(new DayWithoutCourse(
                id: id,
                name: dayWithoutCourseRequest.Name,
                date: DateOnly.FromDateTime(dayWithoutCourseRequest.Date)
                ));
            return Ok(dayWithoutCourse.MapToDto());
        }
        
        #endregion
        
        #region DELETE
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAcademicYear(Guid id)
        {
            await academicYearService.DeleteAcademicYearAsync(id);
            return NoContent();
        }

        [HttpDelete("non-working-days/{id:guid}")]
        public async Task<IActionResult> DeleteDayWithoutCourse(Guid id)
        {
            var response = await dayWithoutCourseService.DeleteDayWithoutCourseAsync(id);
            return Ok(new { message = $"Non Working Day with id : {id} deleted" });
        }
        
        #endregion
    }
}
