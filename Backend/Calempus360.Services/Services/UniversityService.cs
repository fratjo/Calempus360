using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Models;

namespace Calempus360.Services.UniversityService;

public class UniversityService : IUniversityService
{
    private readonly IUniversityRepository _universityRepository;
    
    public UniversityService(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    public async Task<IEnumerable<University>> GetAllAsync()
    {
        var list = await this._universityRepository.GetAllAsync();
        return list;
    }

    public async Task<University> GetByIdAsync(Guid id)
    {
        var u = await this._universityRepository.GetByIdAsync(id);
        return u;
    }

    public async Task<University> PostNewUniversityAsync(University university)
    {
        return await this._universityRepository.PostNewUniversityAsync(university);
    }
    
    public async Task<University> UpdateUniversityAsync(University university)
    {
        return await this._universityRepository.UpdateUniversityAsync(university);
    }
}