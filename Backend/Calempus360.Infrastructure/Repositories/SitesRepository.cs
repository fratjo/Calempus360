using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories;

public class SitesRepository(Calempus360DbContext dbContext) : ISiteRepository
{
    public async Task<IEnumerable<Site>> GetSitesAsync()
    {
        var sites = await dbContext.Sites
                                   .Include(s => s.Classrooms)
                                   .Include(s => s.Equipments)
                                   .ToListAsync();

        return sites.Select(s => s.ToDomainModel());
    }

    public Task<IEnumerable<Site>> GetSitesByUniversityAsync(Guid universityId)
    {
        var sites = dbContext.Sites
                             .Include(s => s.Classrooms)
                             .Include(s => s.Equipments)
                             .Where(s => s.UniversityId == universityId);

        return Task.FromResult<IEnumerable<Site>>((sites.Select(s => s.ToDomainModel())));
    }

    public async Task<Site> GetSiteByIdAsync(Guid id)
    {
        var site = await dbContext.Sites
                                  .Include(s => s.Classrooms)
                                  .Include(s => s.Equipments)
                                  .FirstOrDefaultAsync(s => s.SiteId == id);

        if (site == null) throw new NotFoundException("Site not found");

        return site.ToDomainModel();
    }

    public async Task<Site> CreateSiteAsync(Site site, Guid universityId)
    {
        var entity = site.ToEntity();

        entity.UniversityId = universityId;

        await dbContext.Sites.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<Site> UpdateSiteAsync(Site site)
    {
        var entity = await dbContext.Sites.FirstOrDefaultAsync(s => s.SiteId == site.Id);

        if (entity == null) throw new NotFoundException("Site not found");

        entity.Name      = site.Name;
        entity.Code      = site.Code;
        entity.Address   = site.Address;
        entity.Phone     = site.Phone;
        entity.UpdatedAt = site.UpdatedAt;

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }
}