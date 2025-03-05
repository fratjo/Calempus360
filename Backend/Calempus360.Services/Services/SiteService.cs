using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Models;
using Calempus360.Errors.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Services.Services;

public class SiteService(ISiteRepository siteRepository) : ISiteService
{
    public async Task<IEnumerable<Site>> GetSitesAsync()
    {
        var sites = await siteRepository.GetSitesAsync();
        return sites;
    }

    public async Task<IEnumerable<Site>> GetSitesByUniversityAsync(Guid universityId)
    {
        var sites = await siteRepository.GetSitesByUniversityAsync(universityId);
        return sites;
    }

    public async Task<Site> GetSiteByIdAsync(Guid id)
    {
        var site = await siteRepository.GetSiteByIdAsync(id);
        return site;
    }

    public async Task<Site> CreateSiteAsync(Site site, Guid universityId)
    {
        try
        {
            return await siteRepository.CreateSiteAsync(site, universityId);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Site or one or more site's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Site> UpdateSiteAsync(Site site)
    {
        try
        {
            return await siteRepository.UpdateSiteAsync(site);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Site or one or more site's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteSiteAsync(Guid id)
    {
        return await siteRepository.DeleteSiteAsync(id);
    }

    public async Task<bool> DeleteSiteByUniversityAsync(Guid id)
    {
        var sites = await this.GetSitesByUniversityAsync(id) as List<Site>;
        
        sites?.ForEach(async site =>
        {
            // delete children
            // classrooms
            // groups
            
        });
        
        return await siteRepository.DeleteSiteByUniversityAsync(id);
    }
}