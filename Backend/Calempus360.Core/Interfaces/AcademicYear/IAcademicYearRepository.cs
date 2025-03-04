using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.AcademicYear;

public interface IAcademicYearRepository
{
    Task<IEnumerable<Models.AcademicYear>> GetAcademicYearsAsync();
    Task<Models.AcademicYear> GetAcademicYearByIdAsync(Guid             id);
    Task<Models.AcademicYear> CreateAcademicYearAsync(Models.AcademicYear academicYear);
    Task<Models.AcademicYear> UpdateAcademicYearAsync(Guid              id, Models.AcademicYear academicYear);
    Task DeleteAcademicYearAsync(Guid id);
}