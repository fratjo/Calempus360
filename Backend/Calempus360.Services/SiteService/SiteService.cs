using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Models;

namespace Calempus360.Services.SiteService;

public class SiteService : ISiteService
{
    private readonly ISiteRepository _siteRepository;
    
    public SiteService(ISiteRepository siteRepository)
    {
        _siteRepository = siteRepository;
    }

    public async Task<IEnumerable<Site>> GetSitesAsync()
    {
        var sites = await _siteRepository.GetSitesAsync();
        return sites;
    }

    public async Task<IEnumerable<Site>> GetSitesByUniversityAsync(Guid universityId)
    {
        var sites = await _siteRepository.GetSitesByUniversityAsync(universityId);
        return sites;
    }

    public async Task<Site> GetSiteByIdAsync(Guid id)
    {
        var site = await _siteRepository.GetSiteByIdAsync(id);
        return site;
    }

    public async Task<Site> CreateSiteAsync(Site site, Guid universityId)
    {
        var newSite = await _siteRepository.CreateSiteAsync(site, universityId);
        return newSite;
    }

    public async Task<Site> UpdateSiteAsync(Site site)
    {
        var updatedSite = await _siteRepository.UpdateSiteAsync(site);
        return updatedSite;
    }
}