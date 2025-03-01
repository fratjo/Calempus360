using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.Directory;

public interface IAcademicYearRepository
{
    Task<IEnumerable<AcademicYear>> GetAcademicYearsAsync();
    Task<Models.AcademicYear> GetAcademicYearByIdAsync(string             id);
    Task<Models.AcademicYear> CreateAcademicYearAsync(Models.AcademicYear academicYear);
    Task<Models.AcademicYear> UpdateAcademicYearAsync(string              id, Models.AcademicYear academicYear);
}