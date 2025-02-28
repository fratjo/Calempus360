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
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        #region Get

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await this._universityService.GetAllAsync();
            return Ok(list.Select(u => new GetUniversityOnlyResponse
            {
                Id      = u.Id,
                Name    = u.Name,
                Code    = u.Code,
                Phone   = u.Phone,
                Address = u.Address
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var u = await this._universityService.GetByIdAsync(id);
            return Ok(new GetUniversityOnlyResponse
            {
                Id      = u.Id,
                Name    = u.Name,
                Code    = u.Code,
                Phone   = u.Phone,
                Address = u.Address
            });
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostPutUniversityRequest request)
        {
            var u = new University(
                id: null,
                name: request.Name,
                code: request.Code,
                phone: request.Phone,
                address: request.Address,
                null, null, null, null
            );

            u = await this._universityService.PostNewUniversityAsync(u);

            return CreatedAtAction(nameof(Add), new { id = u.Id }, new GetUniversityOnlyResponse
            {
                Id      = u.Id,
                Name    = u.Name,
                Code    = u.Code,
                Phone   = u.Phone,
                Address = u.Address
            });
        }

        #endregion
        
        #region Put
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] PostPutUniversityRequest request)
        {
            var u = new University(
                id: id,
                name: request.Name,
                code: request.Code,
                phone: request.Phone,
                address: request.Address,
                null, null, null, null
            );

            u = await this._universityService.UpdateUniversityAsync(u);

            return Ok(new GetUniversityOnlyResponse
            {
                Id      = u.Id,
                Name    = u.Name,
                Code    = u.Code,
                Phone   = u.Phone,
                Address = u.Address
            });
        }
        
        #endregion
    }
}