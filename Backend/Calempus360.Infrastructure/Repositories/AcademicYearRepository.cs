using Calempus360.Core.Interfaces.AcademicYear;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories;

public class AcademicYearRepository(Calempus360DbContext dbContext) : IAcademicYearRepository
{
    public async Task<IEnumerable<AcademicYear>> GetAcademicYearsAsync()
    {
        var list = await dbContext.AcademicYears.Include(ac => ac.DaysWithoutCourses).ToListAsync();
        return list.Select(a => a.ToDomainModel());
    }

    public async Task<AcademicYear> GetAcademicYearByIdAsync(Guid id)
    {
        var entity = await dbContext.AcademicYears.Include(ac => ac.DaysWithoutCourses)
                                    .FirstOrDefaultAsync(a => a.AcademicYearId == id);

        if (entity == null) throw new NotFoundException("Academic year not found");

        return entity.ToDomainModel();
    }

    public async Task<AcademicYear> CreateAcademicYearAsync(AcademicYear academicYear)
    {
        var entity = academicYear.ToEntity();

        await dbContext.AcademicYears.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<AcademicYear> UpdateAcademicYearAsync(Guid id, AcademicYear academicYear)
    {
        var entity = academicYear.ToEntity();

        var existingEntity = await dbContext.AcademicYears.FirstOrDefaultAsync(a => a.AcademicYearId == id);

        if (existingEntity == null) throw new NotFoundException("Academic year not found");

        existingEntity.DateStart = entity.DateStart;
        existingEntity.DateEnd   = entity.DateEnd;
        existingEntity.UpdatedAt = DateTime.Now;

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task DeleteAcademicYearAsync(Guid id)
    {
        var entity = await dbContext.AcademicYears.FirstOrDefaultAsync(a => a.AcademicYearId == id);
        
        if (entity == null) throw new NotFoundException("Academic year not found");
        
        dbContext.AcademicYears.Remove(entity);
        
        await dbContext.SaveChangesAsync();
        
        return;
    }
}