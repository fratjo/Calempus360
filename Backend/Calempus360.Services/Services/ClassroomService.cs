using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Models;
using Calempus360.Errors.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Services.Services;

public class ClassroomService(IClassroomRepository classroomRepository) : IClassroomService
{
    public async Task<IEnumerable<Classroom>> GetClassroomsAsync(Guid universityId)
    {
        var classrooms = await classroomRepository.GetClassroomsAsync(universityId);
        return classrooms;
    }

    public async Task<IEnumerable<Classroom>> GetClassroomsBySiteAsync(Guid siteId)
    {
        var classrooms = await classroomRepository.GetClassroomsBySiteAsync(siteId);
        
        return classrooms;
    }

    public async Task<Classroom> GetClassroomByIdAsync(Guid id)
    {
        var classroom = await classroomRepository.GetClassroomByIdAsync(id);
        return classroom;
    }

    public async Task<Classroom> CreateClassroomAsync(Classroom classroom, Guid siteId)
    {
        try
        {
            return await classroomRepository.CreateClassroomAsync(classroom, siteId);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Classroom or one or more classroom's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> AddEquipmentToClassroomAsync(Guid classroomId, Guid equipmentId, Guid academicYearId)
    {
        try
        {
            return await classroomRepository.AddEquipmentToClassroomAsync(classroomId, equipmentId, academicYearId);
        } 
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Equipment already exists in classroom for this academic year");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        try
        {
            return await classroomRepository.UpdateClassroomAsync(classroom);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Classroom or one or more classroom's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteClassroomAsync(Guid id)
    {
        return await classroomRepository.DeleteClassroomAsync(id);
    }

    public async Task<bool> DeleteClassroomsBySiteAsync(Guid siteId)
    {
        var classrooms = await classroomRepository.GetClassroomsBySiteAsync(siteId);
        
        foreach (var classroom in classrooms)
        {
            await classroomRepository.DeleteClassroomAsync(classroom.Id);
        }
        
        return true;
    }

    public async Task<bool> RemoveEquipmentFromClassroomAsync(Guid classroomId, Guid equipmentId, Guid academicYearId)
    {
        return await classroomRepository.RemoveEquipmentFromClassroomAsync(classroomId, equipmentId, academicYearId);
    }
}