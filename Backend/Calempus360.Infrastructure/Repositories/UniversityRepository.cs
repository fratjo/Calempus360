using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories;

public class UniversityRepository : IUniversityRepository
{
    private readonly Calempus360DbContext _context;

    public UniversityRepository(Calempus360DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<University>> GetAllAsync()
    {
        var list = await _context.Universities.ToListAsync();
        return list.Select(x => x.ToDomainModel());
    }

    public async Task<University> GetByIdAsync(Guid id)
    {
        var u = await _context.Universities.FindAsync(id);
        return u.ToDomainModel();
    }

    public async Task<University> PostNewUniversityAsync(University university)
    {
        var entity = university.ToEntity();

        await _context.Universities.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<University> UpdateUniversityAsync(University university)
    {
        var entity = university.ToEntity();

        var existing = await _context.Universities.FindAsync(entity.UniversityId);

        if (existing == null)
        {
            throw new NotFoundException($"University with id {entity.UniversityId} not found");
        }

        existing.Name    = university.Name;
        existing.Code    = university.Code;
        existing.Phone   = university.Phone;
        existing.Address = university.Address;
        existing.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return entity.ToDomainModel();
    }
}