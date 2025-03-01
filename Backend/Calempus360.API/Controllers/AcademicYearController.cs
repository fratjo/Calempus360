using System.Net;
using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.Interfaces.Directory;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/academic-years")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;
        
        public AcademicYearController(IAcademicYearService academicYearService)
        {
            _academicYearService = academicYearService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAcademicYears()
        {
            var academicYears = await _academicYearService.GetAcademicYearsAsync();
            return Ok(academicYears);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAcademicYearById(string id)
        {
            var academicYear = await _academicYearService.GetAcademicYearByIdAsync(id);
            return Ok(academicYear);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAcademicYear([FromBody] PostPutAcademicYearRequest request)
        {   
            var academicYear = new AcademicYear(
                id: request.Id,
                dateStart: DateOnly.FromDateTime(request.DateStart),
                dateEnd: DateOnly.FromDateTime(request.DateEnd),
                null,null,null);
            
            var createdAcademicYear = await _academicYearService.CreateAcademicYearAsync(academicYear);
            return CreatedAtAction(nameof(GetAcademicYearById), new { id = createdAcademicYear.Id }, createdAcademicYear);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcademicYear(string id, [FromBody] PostPutAcademicYearRequest request)
        {
            var academicYear = new AcademicYear(
                id: request.Id,
                dateStart: DateOnly.FromDateTime(request.DateStart),
                dateEnd: DateOnly.FromDateTime(request.DateEnd),
                null,null,null);
            
            academicYear = await _academicYearService.UpdateAcademicYearAsync(id, academicYear);
            return Ok(academicYear);
        }
    }
}
