using Calempus360.Core.Interfaces.Directory;
using Calempus360.Core.Models;

namespace Calempus360.Services.AcademicYearService;

public class AcademciYearService : IAcademicYearService
{
    private readonly IAcademicYearRepository _academicYearRepository;
    
    public AcademciYearService(IAcademicYearRepository academicYearRepository)
    {
        _academicYearRepository = academicYearRepository;
    }


    public async Task<IEnumerable<AcademicYear>> GetAcademicYearsAsync()
    {
        var list = await _academicYearRepository.GetAcademicYearsAsync();
        return list;
    }

    public async Task<AcademicYear> GetAcademicYearByIdAsync(string id)
    {
        var a = await _academicYearRepository.GetAcademicYearByIdAsync(id);
        return a;
    }

    public async Task<AcademicYear> CreateAcademicYearAsync(AcademicYear academicYear)
    {
        var a = await _academicYearRepository.CreateAcademicYearAsync(academicYear);
        return a;
    }

    public async Task<AcademicYear> UpdateAcademicYearAsync(string id, AcademicYear academicYear)
    {
        var a = await _academicYearRepository.UpdateAcademicYearAsync(id, academicYear);
        return a;
    }
}