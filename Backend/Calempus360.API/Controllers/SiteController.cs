using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calempus360.API.Controllers
{
    [Route("api/university/{universityId:guid}/sites")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;
        
        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSitesByUniversityIdAsync(Guid universityId)
        {
            var sites = await _siteService.GetSitesByUniversityAsync(universityId);
            return Ok(sites);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSiteByIdAsync(Guid id)
        {
            var site = await _siteService.GetSiteByIdAsync(id);
            return Ok(site);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSiteAsync(Guid universityId, [FromBody] PostPutSiteRequest request)
        {
            var site = await _siteService.CreateSiteAsync(new Site
            (
                id: null,
                name    : request.Name,
                code    : request.Code,
                address : request.Address,
                phone   : request.Phone,
                null,null,null, null, null
            ), universityId);
            return Ok(site);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSiteAsync(Guid id, [FromBody] PostPutSiteRequest request)
        {
            var site = await _siteService.UpdateSiteAsync(new Site
            (
                id      : id,
                name    : request.Name,
                code    : request.Code,
                address : request.Address,
                phone   : request.Phone,
                null,null,null, null, null
            ));
            return Ok(site);
        }
    }
}
