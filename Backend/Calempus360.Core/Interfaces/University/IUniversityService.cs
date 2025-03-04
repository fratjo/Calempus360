using Calempus360.Core.DTOs.Requests;

namespace Calempus360.Core.Interfaces.University;

public interface IUniversityService
{
    Task<IEnumerable<Models.University>> GetAllAsync();
    Task<Models.University>              GetByIdAsync(Guid                        id);
    Task<Models.University>             PostNewUniversityAsync(Models.University university);
    Task<Models.University>              UpdateUniversityAsync(Models.University university);
    Task DeleteUniversityAsync(Guid id);
}