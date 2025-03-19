using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/sites")]
    [ApiController]
    public class SiteController(ISiteService siteService) : ControllerBase
    {
        #region GET

        [HttpGet]
        public async Task<IActionResult> GetSitesByUniversityIdAsync([FromQuery] Guid universityId)
        {
            var sites = await siteService.GetSitesByUniversityAsync(universityId);
            return Ok(sites.Select(s => s?.MapToDto()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSiteByIdAsync(Guid id)
        {
            var site = await siteService.GetSiteByIdAsync(id);
            return Ok(site.MapToDto());
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> CreateSiteAsync([FromQuery] Guid universityId, [FromBody] SiteRequestDto requestDto)
        {
            var site = await siteService.CreateSiteAsync(new Site
            (
                name: requestDto.Name,
                code: requestDto.Code,
                address: requestDto.Address,
                phone: requestDto.Phone,
                schedules: requestDto.Schedules.Select(s => new Schedule
                (
                    timeStart: s.TimeStart,
                    timeEnd: s.TimeEnd,
                    dayOfWeek: s.DayOfWeek
                )).ToList()
            ), universityId);

            return Ok(site.MapToDto());
        }

        #endregion

        #region PUT

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSiteAsync(Guid id, [FromBody] SiteRequestDto requestDto)
        {
            var site = await siteService.UpdateSiteAsync(new Site
            (
                name: requestDto.Name,
                code: requestDto.Code,
                address: requestDto.Address,
                phone: requestDto.Phone,
                id: id,
                schedules: requestDto.Schedules.Select(s => new Schedule
                (
                    timeStart: s.TimeStart,
                    timeEnd: s.TimeEnd,
                    dayOfWeek: s.DayOfWeek
                )).ToList()
            ));

            return Ok(site.MapToDto());
        }

        #endregion

        #region DELETE

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSiteAsync(Guid id)
        {
            await siteService.DeleteSiteAsync(id);
            return NoContent();
        }

        #endregion
    }
}
