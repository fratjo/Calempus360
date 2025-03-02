using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Interfaces.AcademicYear;
using Calempus360.Core.Models;
using Calempus360.Errors.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Services.Services;

public class AcademicYearService(IAcademicYearRepository academicYearRepository) : IAcademicYearService
{
    public async Task<IEnumerable<AcademicYear>> GetAcademicYearsAsync()
    {
        var list = await academicYearRepository.GetAcademicYearsAsync();
        return list;
    }

    public async Task<AcademicYear> GetAcademicYearByIdAsync(string id)
    {
        var a = await academicYearRepository.GetAcademicYearByIdAsync(id);
        return a;
    }

    public async Task<AcademicYear> CreateAcademicYearAsync(AcademicYear academicYear)
    {
        try
        {
            return await academicYearRepository.CreateAcademicYearAsync(academicYear);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("AcademicYear or one or more academicYear's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<AcademicYear> UpdateAcademicYearAsync(string id, AcademicYear academicYear)
    {
        try
        {
            return await academicYearRepository.UpdateAcademicYearAsync(id, academicYear);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("AcademicYear or one or more academicYear's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}