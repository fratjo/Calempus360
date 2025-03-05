using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories;

public class UniversityRepository(Calempus360DbContext context) : IUniversityRepository
{
    public async Task<IEnumerable<University>> GetAllAsync()
    {
        var list = await context.Universities
                                 .Include(u => u.Sites)
                                 .ToListAsync();
        
        return list.Select(x => x.ToDomainModel());
    }

    public async Task<University> GetUniversityByIdAsync(Guid id)
    {
        var u = await context.Universities
                              .Include(u => u.Sites)
                              .FirstOrDefaultAsync(u => u.UniversityId == id);
        
        if (u == null) throw new NotFoundException("University not found");
        
        return u.ToDomainModel();
    }

    public async Task<University> GetUniversityByNameAsync(string name)
    {
        var u = await context.Universities
                             .FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower());

        if (u == null) throw new NotFoundException("University not found");
        
        return u.ToDomainModel();
    }

    public async Task<University> PostNewUniversityAsync(University university)
    {
        var entity = university.ToEntity();

        await context.Universities.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<University> UpdateUniversityAsync(University university)
    {
        var entity = university.ToEntity();

        var existing = await context.Universities.FindAsync(entity.UniversityId);

        if (existing == null)
        {
            throw new NotFoundException($"University with id {entity.UniversityId} not found");
        }

        existing.Name      = university.Name;
        existing.Code      = university.Code;
        existing.Phone     = university.Phone;
        existing.Address   = university.Address;
        existing.UpdatedAt = DateTime.Now;

        await context.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<bool> DeleteUniversityAsync(Guid id)
    {
        var entity = await context.Universities.FirstOrDefaultAsync(x => x.UniversityId == id);
        
        if (entity == null) throw new NotFoundException("University not found");
        
        context.Universities.Remove(entity);
        
        await context.SaveChangesAsync();
        
        return true;
    }
}