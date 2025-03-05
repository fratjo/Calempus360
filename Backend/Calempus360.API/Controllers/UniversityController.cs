using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/universities")]
    [ApiController]
    public class UniversityController(IUniversityService universityService) : ControllerBase
    {
        #region Get

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await universityService.GetAllAsync();
            return Ok(list.Select(u => u.MapToDto()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var u = await universityService.GetByIdAsync(id);
            return Ok(u.MapToDto());
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UniversityRequestDto requestDto)
        {
            var u = new University(
                name: requestDto.Name,
                code: requestDto.Code,
                phone: requestDto.Phone,
                address: requestDto.Address
            );

            u = await universityService.PostNewUniversityAsync(u);

            return CreatedAtAction(nameof(Add), new { id = u.Id }, u.MapToDto());
        }

        #endregion
        
        #region Put
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UniversityRequestDto requestDto)
        {
            var u = new University(
                name: requestDto.Name,
                code: requestDto.Code,
                phone: requestDto.Phone,
                address: requestDto.Address,
                id
            );

            u = await universityService.UpdateUniversityAsync(u);

            return Ok(u.MapToDto());
        }
        
        #endregion
        
        #region Delete
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await universityService.DeleteUniversityAsync(id);
            return NoContent();
        }
        
        #endregion
    }
}