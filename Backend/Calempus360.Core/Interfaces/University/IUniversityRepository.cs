using Calempus360.Core.Models;

namespace Calempus360.Core.Interfaces.University;

public interface IUniversityRepository
{
    Task<IEnumerable<Models.University>> GetAllAsync();
    Task<Models.University> GetUniversityByIdAsync(Guid id);
    Task<Models.University> GetUniversityByNameAsync(string name);
    Task<Models.University> PostNewUniversityAsync(Models.University university);
    Task<Models.University> UpdateUniversityAsync(Models.University university);
    Task DeleteUniversityAsync(Guid id);
}