using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Errors.Mappers;
using Calempus360.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Services.Services;

public class UniversityService(
    IUniversityRepository universityRepository,
    ISiteService sitesService
) : IUniversityService
{
    public async Task<IEnumerable<University>> GetAllAsync()
    {
        var list = await universityRepository.GetAllAsync();
        return list;
    }

    public async Task<University> GetByIdAsync(Guid id)
    {
        var u = await universityRepository.GetUniversityByIdAsync(id);
        return u;
    }

    public async Task<University> PostNewUniversityAsync(University university)
    {
        try
        {
            return await universityRepository.PostNewUniversityAsync(university);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("University or one or more university's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<University> UpdateUniversityAsync(University university)
    {
        try
        {
            return await universityRepository.UpdateUniversityAsync(university);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("University or one or more university's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteUniversityAsync(Guid id)
    {
        if(!await sitesService.DeleteSiteByUniversityAsync(id)) throw new InvalidOperationException("Error deleting sites");
        
        await universityRepository.DeleteUniversityAsync(id);
        return true;
    }
}