namespace Calempus360.Core.Interfaces.Site;

public interface ISiteService
{
    Task<IEnumerable<Models.Site>> GetSitesAsync();
    Task<IEnumerable<Models.Site>> GetSitesByUniversityAsync(Guid universityId);
    Task<Models.Site>              GetSiteByIdAsync(Guid          id);
    Task<Models.Site>              CreateSiteAsync(Models.Site    site, Guid universityId);
    Task<Models.Site>              UpdateSiteAsync(Models.Site    site);
    Task<bool>                         DeleteSiteAsync(Guid           id);
    Task<bool>                           DeleteSiteByUniversityAsync(Guid           id);
}