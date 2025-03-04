using System.Net;
using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.AcademicYear;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/academic-years")]
    [ApiController]
    public class AcademicYearController(IAcademicYearService academicYearService) : ControllerBase
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
        
        #endregion
        
        #region DELETE
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAcademicYear(Guid id)
        {
            await academicYearService.DeleteAcademicYearAsync(id);
            return NoContent();
        }
        
        #endregion
    }
}
