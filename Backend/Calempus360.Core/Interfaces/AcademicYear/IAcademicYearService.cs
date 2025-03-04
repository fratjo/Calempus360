namespace Calempus360.Core.Interfaces.AcademicYear;

public interface IAcademicYearService
{
    Task<IEnumerable<Models.AcademicYear>> GetAcademicYearsAsync();
    Task<Models.AcademicYear> GetAcademicYearByIdAsync(Guid id);
    Task<Models.AcademicYear> CreateAcademicYearAsync(Models.AcademicYear academicYear);
    Task<Models.AcademicYear> UpdateAcademicYearAsync(Guid id, Models.AcademicYear academicYear);
    Task DeleteAcademicYearAsync(Guid id);
}