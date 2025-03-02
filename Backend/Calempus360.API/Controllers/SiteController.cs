using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/university/{universityId:guid}/sites")]
    [ApiController]
    public class SiteController(ISiteService siteService) : ControllerBase
    {
        #region GET
        
        [HttpGet]
        public async Task<IActionResult> GetSitesByUniversityIdAsync(Guid universityId)
        {
            var sites = await siteService.GetSitesByUniversityAsync(universityId);
            return Ok(sites.Select(s => s.MapToDto()));
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
        public async Task<IActionResult> CreateSiteAsync(Guid universityId, [FromBody] SiteRequest request)
        {
            var site = await siteService.CreateSiteAsync(new Site
            (
                name    : request.Name,
                code    : request.Code,
                address : request.Address,
                phone   : request.Phone
            ), universityId);
            
            return Ok(site.MapToDto());
        }
        
        #endregion
        
        #region PUT
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSiteAsync(Guid id, [FromBody] SiteRequest request)
        {
            var site = await siteService.UpdateSiteAsync(new Site
            (
                name    : request.Name,
                code    : request.Code,
                address : request.Address,
                phone   : request.Phone,
                id      : id
            ));
            
            return Ok(site.MapToDto());
        }
        
        #endregion
    }
}
