using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Core.Models;
using Calempus360.Errors.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Services.Services;

public class EquipmentService(IEquipmentRepository equipmentRepository, IClassroomService classroomService) : IEquipmentService
{
    # region EquipmentType

    public async Task<IEnumerable<EquipmentType>> GetEquipmentTypesAsync()
    {
        var equipmentTypes = await equipmentRepository.GetEquipmentTypesAsync();

        return equipmentTypes;
    }

    public async Task<EquipmentType> GetEquipmentTypeByIdAsync(Guid id)
    {
        var equipmentType = await equipmentRepository.GetEquipmentTypeByIdAsync(id);

        return equipmentType;
    }

    public async Task<EquipmentType> CreateEquipmentTypeAsync(EquipmentType equipmentType)
    {
        try
        {
            return await equipmentRepository.CreateEquipmentTypeAsync(equipmentType);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Site or one or more site's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<EquipmentType> UpdateEquipmentTypeAsync(EquipmentType equipmentType)
    {
        try
        {
            return await equipmentRepository.UpdateEquipmentTypeAsync(equipmentType);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Site or one or more site's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> DeleteEquipmentTypeByIdAsync(Guid id)
    {
        // check if one or more equipment is using this equipment type
        var equipments = await equipmentRepository.GetEquipmentsByEquipmentTypeAsync(id);

        if (equipments.Any())
            throw new ValidationException("One or more equipment is using this equipment type");


        return await equipmentRepository.DeleteEquipmentTypeByIdAsync(id);
    }

    #endregion

    #region Equipment

    public Task<IEnumerable<Equipment>> GetEquipmentsAsync(Guid? universityId, Guid? siteId, Guid? classroomId, Guid? equipmentTypeId)
    {
        var equipments = equipmentRepository.GetEquipmentsAsync(universityId, siteId, classroomId, equipmentTypeId);

        return equipments;
    }
    public async Task<IEnumerable<Equipment>> GetEquipmentsByUniversityAsync(Guid universityId)
    {
        var equipments = await equipmentRepository.GetEquipmentsByUniversityAsync(universityId);

        return equipments;
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsBySiteAsync(Guid siteId)
    {
        var equipments = await equipmentRepository.GetEquipmentsBySiteAsync(siteId);

        return equipments;
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByClassroomAsync(Guid classroomId, Guid? academicYearId)
    {
        var equipments = await equipmentRepository.GetEquipmentsByClassroomAsync(classroomId, academicYearId);

        return equipments;
    }

    public async Task<Equipment> GetEquipmentByIdAsync(Guid id)
    {
        var equipment = await equipmentRepository.GetEquipmentByIdAsync(id);

        return equipment;
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByEquipmentTypeAsync(Guid equipmentTypeId)
    {
        var equipments = await equipmentRepository.GetEquipmentsByEquipmentTypeAsync(equipmentTypeId);

        return equipments;
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment, Guid equipmentTypeId, Guid? siteId, Guid universityId)
    {
        try
        {
            var equipmentType = await equipmentRepository.GetEquipmentTypeByIdAsync(equipmentTypeId)!;

            equipment.SetEquipmentType(equipmentType);

            return await equipmentRepository.CreateEquipmentAsync(equipment, siteId, universityId);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Equipment or one or more equipment's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment, Guid equipmentTypeId, Guid universityId)
    {
        try
        {
            var equipmentType = await equipmentRepository.GetEquipmentTypeByIdAsync(equipmentTypeId)!;

            equipment.SetEquipmentType(equipmentType);

            return await equipmentRepository.CreateEquipmentAsync(equipment, universityId);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Equipment or one or more equipment's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Equipment> UpdateEquipmentAsync(Equipment equipment, Guid equipmentTypeId, Guid? classroomId)
    {
        try
        {
            var equipmentType = await equipmentRepository.GetEquipmentTypeByIdAsync(equipmentTypeId)!;

            equipment.SetEquipmentType(equipmentType);

            if (classroomId != null)
            {
                var classroom = await classroomService.GetClassroomByIdAsync(classroomId!.Value);

                if (classroom != null)
                    equipment.SetClassroom(classroom);
            }

            return await equipmentRepository.UpdateEquipmentAsync(equipment);
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is SqlException sqlException)
                sqlException.MapSqlException();
            throw new ValidationException("Equipment or one or more equipment's field already exists");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> ChangeEquipmentClassroomAsync(Guid equipmentId, Guid classroomId, Guid academciYearId)
    {
        var result = await equipmentRepository.ChangeEquipmentClassroomAsync(equipmentId, classroomId, academciYearId);
        return result;
    }

    public async Task<bool> DeleteEquipmentAsync(Guid id)
    {
        return await equipmentRepository.DeleteEquipmentAsync(id);
    }

    public async Task<bool> DeleteEquipmentsByUniversityAsync(Guid universityId)
    {
        var equipments = await equipmentRepository.GetEquipmentsByUniversityAsync(universityId);

        foreach (var equipment in equipments)
        {
            await equipmentRepository.DeleteEquipmentAsync(equipment.Id);
        }

        return true;
    }

    public async Task<bool> DeleteEquipmentsBySiteAsync(Guid siteId)
    {
        var equipments = await equipmentRepository.GetEquipmentsBySiteAsync(siteId);

        foreach (var equipment in equipments)
        {
            await equipmentRepository.DeleteEquipmentAsync(equipment.Id);
        }

        return true;
    }


    #endregion
}