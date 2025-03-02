using Calempus360.Core.Interfaces.AcademicYear;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories;

public class AcademicYearRepository : IAcademicYearRepository
{
    private readonly Calempus360DbContext _dbContext;
    
    public AcademicYearRepository(Calempus360DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<AcademicYear>> GetAcademicYearsAsync()
    {
        var list = await _dbContext.AcademicYears.ToListAsync();
        return list.Select(a => a.ToDomainModel());
    }

    public async Task<AcademicYear> GetAcademicYearByIdAsync(string id)
    {
        var entity = await _dbContext.AcademicYears.FirstOrDefaultAsync(a => a.AcademicYearId == id);

        if (entity == null) throw new NotFoundException("Academic year not found");
        
        return entity.ToDomainModel();
    }

    public async Task<AcademicYear> CreateAcademicYearAsync(AcademicYear academicYear)
    {
        var entity = academicYear.ToEntity();
        
        await _dbContext.AcademicYears.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        
        return entity.ToDomainModel();
    }

    public async Task<AcademicYear> UpdateAcademicYearAsync(string id, AcademicYear academicYear)
    {
        var entity = academicYear.ToEntity();
        
        var existingEntity = await _dbContext.AcademicYears.FirstOrDefaultAsync(a => a.AcademicYearId == id);
        
        if (existingEntity == null) throw new NotFoundException("Academic year not found");
        
        existingEntity.DateStart = entity.DateStart;
        existingEntity.DateEnd = entity.DateEnd;
        existingEntity.UpdatedAt = DateTime.Now;
        
        await _dbContext.SaveChangesAsync();
        
        return entity.ToDomainModel();
    }
}