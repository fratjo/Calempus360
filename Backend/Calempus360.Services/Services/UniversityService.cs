using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Errors.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Services.Services;

public class UniversityService(IUniversityRepository universityRepository) : IUniversityService
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
}