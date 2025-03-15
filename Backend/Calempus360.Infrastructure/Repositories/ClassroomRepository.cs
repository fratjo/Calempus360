using Calempus360.Core.Interfaces.Classroom;
using Calempus360.Core.Models;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories;

public class ClassroomRepository(Calempus360DbContext dbContext) : IClassroomRepository
{
    public async Task<IEnumerable<Classroom>> GetClassroomsAsync(Guid? universityId, Guid? siteId)
    {
        var classrooms = from c in await dbContext.Classrooms
                                    .Include(c => c.SiteEntity)
                                    .Include(c => c.ClassroomEquipments)!
                                    .ThenInclude(ce => ce.EquipmentEntity)
                                    .ThenInclude(e => e.EquipmentTypeEntity)
                                    .ToListAsync()
                         where (universityId == null || c.SiteEntity!.UniversityId == universityId) &&
                               (siteId == null || c.SiteId == siteId)
                         select c;

        return classrooms.Select(c => c.ToDomainModel());
    }

    public async Task<Classroom> GetClassroomByIdAsync(Guid id)
    {
        var classroom = await dbContext.Classrooms
                                       .Include(c => c.ClassroomEquipments)!
                                       .ThenInclude(ce => ce.EquipmentEntity)
                                       .ThenInclude(e => e.EquipmentTypeEntity)
                                      .FirstOrDefaultAsync(c => c.ClassroomId == id);

        if (classroom == null) throw new NotFoundException("Classroom not found");

        return classroom.ToDomainModel();
    }

    public async Task<Classroom> CreateClassroomAsync(Classroom classroom, Guid siteId)
    {
        var entity = classroom.ToEntity();

        entity.SiteId = siteId;

        await dbContext.Classrooms.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<bool> AddEquipmentToClassroomAsync(Guid classroomId, Guid equipmentId, Guid academicYearId)
    {
        if (!await dbContext.Classrooms.AnyAsync(c => c.ClassroomId == classroomId)) throw new NotFoundException("Classroom not found");
        if (!await dbContext.Equipments.AnyAsync(e => e.EquipmentId == equipmentId)) throw new NotFoundException("Equipment not found");
        if (!await dbContext.AcademicYears.AnyAsync(a => a.AcademicYearId == academicYearId)) throw new NotFoundException("Academic year not found");

        var entity = new ClassroomEquipmentEntity()
        {
            ClassroomId = classroomId,
            EquipmentId = equipmentId,
            AcademicYearId = academicYearId
        };

        await dbContext.ClassroomsEquipments.AddAsync(entity);

        await dbContext.SaveChangesAsync();

        return true;

    }

    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        var entity = await dbContext.Classrooms.FirstOrDefaultAsync(c => c.ClassroomId == classroom.Id);

        if (entity == null) throw new NotFoundException("Classroom not found");

        entity.Name = classroom.Name;
        entity.Code = classroom.Code;
        entity.Capacity = classroom.Capacity;
        entity.UpdatedAt = classroom.UpdatedAt;

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<bool> DeleteClassroomAsync(Guid id)
    {
        var entity = await dbContext.Classrooms.FirstOrDefaultAsync(c => c.ClassroomId == id);

        if (entity == null) throw new NotFoundException("Classroom not found");

        dbContext.Classrooms.Remove(entity);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteClassroomsBySiteAsync(Guid siteId)
    {
        var classrooms = await dbContext.Classrooms.Where(c => c.SiteId == siteId).ToListAsync();

        dbContext.Classrooms.RemoveRange(classrooms);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveEquipmentFromClassroomAsync(Guid classroomId, Guid equipmentId, Guid academicYearId)
    {
        if (!await dbContext.Classrooms.AnyAsync(c => c.ClassroomId == classroomId)) throw new NotFoundException("Classroom not found");
        if (!await dbContext.Equipments.AnyAsync(e => e.EquipmentId == equipmentId)) throw new NotFoundException("Equipment not found");
        if (!await dbContext.AcademicYears.AnyAsync(a => a.AcademicYearId == academicYearId)) throw new NotFoundException("Academic year not found");

        var entity = await dbContext.ClassroomsEquipments.FirstOrDefaultAsync(ce => ce.ClassroomId == classroomId && ce.EquipmentId == equipmentId && ce.AcademicYearId == academicYearId);

        if (entity == null) throw new NotFoundException("ClassroomEquipment not found");

        dbContext.ClassroomsEquipments.Remove(entity);

        await dbContext.SaveChangesAsync();

        return true;
    }
}