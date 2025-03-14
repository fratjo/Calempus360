using System.Runtime.ConstrainedExecution;
using Calempus360.Core.Interfaces.Equipment;
using Calempus360.Core.Models;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories;

public class EquipmentRepository(Calempus360DbContext dbContext) : IEquipmentRepository
{
    #region EquipmentType

    public async Task<IEnumerable<EquipmentType>> GetEquipmentTypesAsync()
    {
        var equipmentTypes = await dbContext.EquipmentTypes.ToListAsync();

        return equipmentTypes.Select(eqt => eqt.ToDomainModel());
    }

    public async Task<EquipmentType> GetEquipmentTypeByIdAsync(Guid id)
    {
        var equipmentType = await dbContext.EquipmentTypes.FirstOrDefaultAsync(eqt => eqt.EquipmentTypeId == id);

        if (equipmentType == null) throw new NotFoundException("EquipmentType type not found");

        return equipmentType.ToDomainModel();
    }

    public async Task<EquipmentType> CreateEquipmentTypeAsync(EquipmentType equipmentType)
    {
        var entity = equipmentType.ToEntity();

        dbContext.EquipmentTypes.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<EquipmentType> UpdateEquipmentTypeAsync(EquipmentType equipmentType)
    {
        var equipmentTypeEntity = await dbContext.EquipmentTypes.FindAsync(equipmentType.Id);

        if (equipmentTypeEntity == null) throw new NotFoundException("Equipment type not found");

        equipmentTypeEntity.Name = equipmentType.Name;
        equipmentTypeEntity.Code = equipmentType.Code;
        equipmentTypeEntity.Description = equipmentType.Description;
        equipmentTypeEntity.UpdatedAt = DateTime.Now;

        await dbContext.SaveChangesAsync();

        return equipmentTypeEntity.ToDomainModel();
    }

    public async Task<bool> DeleteEquipmentTypeByIdAsync(Guid id)
    {
        var equipmentType = await dbContext.EquipmentTypes.FindAsync(id);

        if (equipmentType == null) throw new NotFoundException("Equipment type not found");

        dbContext.EquipmentTypes.Remove(equipmentType);

        await dbContext.SaveChangesAsync();

        return true;
    }

    #endregion

    #region Equipment

    public async Task<IEnumerable<Equipment>> GetEquipmentsAsync(Guid? universityId, Guid? siteId, Guid? classroomId, Guid? equipmentTypeId)
    {
        var equipments = from eq in await dbContext.Equipments
                                    .Include(eq => eq.EquipmentTypeEntity)
                                    .Include(eq => eq.ClassroomEquipments)!
                                        .ThenInclude(ce => ce.ClassroomEntity)
                                    .Include(eq => eq.UniversitySiteEquipmentEntity)
                                    .ToListAsync()
                         where (universityId == null || eq.UniversitySiteEquipmentEntity.UniversityId == universityId) &&
                               (siteId == null || eq.UniversitySiteEquipmentEntity.SiteId == siteId) &&
                               (classroomId == null || eq.ClassroomEquipments!.Any(ce => ce.ClassroomId == classroomId)) &&
                               (equipmentTypeId == null || eq.EquipmentTypeEntity!.EquipmentTypeId == equipmentTypeId)
                         select eq;

        return equipments.Select(eq => eq.ToDomainModel());
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByUniversityAsync(Guid universityId)
    {
        var equipments = await dbContext.Equipments
                                        .Include(eq => eq.EquipmentTypeEntity)
                                        .Include(eq => eq.UniversitySiteEquipmentEntity)
                                        .Include(eq => eq.ClassroomEquipments)!
                                        .ThenInclude(ce => ce.ClassroomEntity)
                                        .ToListAsync();

        var res = equipments.Select(eq => eq.UniversitySiteEquipmentEntity.UniversityId == universityId).ToList();

        return equipments.Select(eq => eq.ToDomainModel());
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsBySiteAsync(Guid siteId)
    {
        var equipments = await dbContext.Equipments
                                        .Include(eq => eq.EquipmentTypeEntity)
                                        .Include(eq => eq.UniversitySiteEquipmentEntity)
                                        .Include(eq => eq.ClassroomEquipments)!
                                        .ThenInclude(ce => ce.ClassroomEntity)
                                        .ToListAsync();

        var res = equipments.Select(eq => eq.UniversitySiteEquipmentEntity.SiteId == siteId).ToList();

        return equipments.Select(eq => eq.ToDomainModel());
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByClassroomAsync(Guid classroomId, Guid? academicYearId)
    {
        var equipments = await dbContext.Equipments
                                        .Include(eq => eq.EquipmentTypeEntity)
                                        .Include(eq => eq.ClassroomEquipments)!
                                        .ThenInclude(ce => ce.ClassroomEntity)
                                        .ToListAsync();

        var res = equipments.Select(eq => eq.ClassroomEquipments?.Where(ce =>
                                                                                ce.ClassroomId == classroomId && (academicYearId == null ||
                                                                                ce.AcademicYearId == academicYearId))).ToList();

        return equipments.Select(eq => eq.ToDomainModel());
    }

    public async Task<IEnumerable<Equipment>> GetEquipmentsByEquipmentTypeAsync(Guid equipmentTypeId)
    {
        var equipments = await dbContext.Equipments
                                        .Include(eq => eq.EquipmentTypeEntity)
                                        .Where(eq => eq.EquipmentTypeEntity!.EquipmentTypeId == equipmentTypeId)
                                        .ToListAsync();

        return equipments.Select(eq => eq.ToDomainModel());
    }

    public async Task<Equipment> GetEquipmentByIdAsync(Guid id)
    {
        var equipment = await dbContext.Equipments
                                       .Include(eq => eq.EquipmentTypeEntity)
                                       .Include(eq => eq.ClassroomEquipments)!
                                       .ThenInclude(ce => ce.ClassroomEntity)
                                       .FirstOrDefaultAsync(eq => eq.EquipmentId == id);

        if (equipment == null) throw new NotFoundException("Equipment not found");

        return equipment.ToDomainModel();
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment, Guid? siteId, Guid universityId)
    {
        var entity = equipment.ToEntity();

        entity.UniversitySiteEquipmentEntity = new UniversitySiteEquipmentEntity
        {
            SiteId = siteId,
            UniversityId = universityId
        };

        dbContext.Equipments.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment, Guid universityId)
    {
        var entity = equipment.ToEntity();

        entity.UniversitySiteEquipmentEntity = new UniversitySiteEquipmentEntity
        {
            UniversityId = universityId
        };

        dbContext.Equipments.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<Equipment> UpdateEquipmentAsync(Equipment equipment)
    {
        var entity = await dbContext.Equipments
                            .Include(e => e.ClassroomEquipments)
                            .Include(e => e.EquipmentTypeEntity)
                            .Include(e => e.UniversitySiteEquipmentEntity)
                            .Where(e => e.EquipmentId == equipment.Id)
                            .FirstOrDefaultAsync();

        if (entity == null) throw new NotFoundException("Equipment not found");

        entity.Name = equipment.Name;
        entity.Code = equipment.Code;
        entity.Brand = equipment.Brand;
        entity.Model = equipment.Model;
        entity.Description = equipment.Description;
        entity.UpdatedAt = DateTime.Now;

        entity.EquipmentTypeEntity = await dbContext.EquipmentTypes.FindAsync(equipment.EquipmentType!.Id);

        var classroomEquipments = entity.ClassroomEquipments.Where(ce => ce.EquipmentId == equipment.Id).FirstOrDefault();

        // If exists, remove the current classroom equipment
        if (classroomEquipments != null)
        {
            dbContext.ClassroomsEquipments.Remove(classroomEquipments);
        }

        // If the equipment has a classroom, add the new classroom equipment
        if (equipment.Classroom != null)
        {
            var newClassroomEquipment = new ClassroomEquipmentEntity
            {
                EquipmentId = equipment.Id,
                ClassroomId = equipment.Classroom.Id,
                AcademicYearId = Guid.Parse("5068f7e9-eb74-45e4-a13c-2d5d95548c23")
            };

            dbContext.ClassroomsEquipments.Add(newClassroomEquipment);
        }

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<bool> ChangeEquipmentClassroomAsync(Guid equipmentId, Guid classroomId, Guid academciYearId)
    {
        if (!await dbContext.Classrooms.AnyAsync(c => c.ClassroomId == classroomId))
            throw new NotFoundException("Classroom not found");
        if (!await dbContext.Equipments.AnyAsync(e => e.EquipmentId == equipmentId))
            throw new NotFoundException("Equipment not found");
        if (!await dbContext.AcademicYears.AnyAsync(ay => ay.AcademicYearId == academciYearId))
            throw new NotFoundException("Academic year not found");

        var entity = await dbContext.ClassroomsEquipments.Where(ce => ce.EquipmentId == equipmentId && ce.AcademicYearId == academciYearId).FirstOrDefaultAsync();

        dbContext.ClassroomsEquipments.Remove(entity);

        entity = new ClassroomEquipmentEntity
        {
            EquipmentId = equipmentId,
            ClassroomId = classroomId,
            AcademicYearId = academciYearId,
        };

        dbContext.ClassroomsEquipments.Add(entity);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteEquipmentAsync(Guid id)
    {
        var entity = await dbContext.Equipments.FindAsync(id);

        if (entity == null) throw new NotFoundException("Equipment not found");

        dbContext.Equipments.Remove(entity);

        await dbContext.SaveChangesAsync();

        return true;
    }

    #endregion
}