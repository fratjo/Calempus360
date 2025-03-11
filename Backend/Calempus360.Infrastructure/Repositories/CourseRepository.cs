using Calempus360.Core.Interfaces.Course;
using Calempus360.Core.Models;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly Calempus360DbContext _context;

        public CourseRepository( Calempus360DbContext context)
        {
            _context = context;
        }
        public async Task<Course> AddCourseAsync(Course course, Guid academicYear, Guid universityId, Dictionary<Guid, int> equipmentType)
        {
            var universityEntity = await _context.Universities.FindAsync(universityId);
            if (universityEntity == null) throw new NotFoundException("University not found!");
            var academicYearEntity = await _context.AcademicYears.FindAsync(academicYear);
            if (academicYearEntity == null) throw new NotFoundException("Academic Year not found!");
            var entity = course.ToEntity();

            if(course.EquipmentTypes is not null)
            {
                foreach (var equipmentTypeId in equipmentType)
                {
                    var equipmentEntity = await _context.EquipmentTypes.FindAsync(equipmentTypeId);
                    if (equipmentEntity == null) throw new NotFoundException("Equipment Type not found!");
                    entity.EquipmentTypes.Add(
                        new CourseEquipmentTypeEntity
                        {
                            AcademicYearEntity = academicYearEntity,
                            CourseEntity = entity,
                            EquipmentTypeEntity = equipmentEntity,
                            UniversityEntity = universityEntity,
                            Quantity = equipmentTypeId.Value
                        });
                }
            }

            await _context.Courses.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ToDomainModel(); 
        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var entity = await _context.Courses.FindAsync(id);
            if (entity == null) throw new NotFoundException("Course not found");
            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            var entities = await _context.Courses
                .Include(c => c.OptionsCourses)
                    .ThenInclude(oc => oc.OptionEntity)
                .Include(c => c.OptionsCourses)
                    .ThenInclude(oc => oc.AcademicYearEntity)
                .Include(c => c.EquipmentTypes)
                    .ThenInclude(et => et.EquipmentTypeEntity)
                .Include(c => c.Sessions)
                .ToListAsync();

            return entities.Select(c => c.ToDomainModel());
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            var entity = await _context.Courses
                        .Include(c => c.OptionsCourses)
                            .ThenInclude(oc => oc.OptionEntity)
                        .Include(c => c.OptionsCourses)
                            .ThenInclude(oc => oc.AcademicYearEntity)
                        .Include(c => c.EquipmentTypes)
                            .ThenInclude(et => et.EquipmentTypeEntity)
                        .Include(c => c.Sessions)
                        .FirstOrDefaultAsync(c => c.CourseId == id);

            if (entity == null) throw new NotFoundException("Course not found");
            return entity.ToDomainModel();
        }

        public async Task<Course> UpdateCourseAsync(Course course, Guid academicYear, Dictionary<Guid, int> equipmentType, Guid universityId)
        {
            var entity = await _context.Courses
                        .Include(c => c.OptionsCourses)
                            .ThenInclude(oc => oc.OptionEntity)
                        .Include(c => c.OptionsCourses)
                            .ThenInclude(oc => oc.AcademicYearEntity)
                        .Include(c => c.EquipmentTypes)
                            .ThenInclude(et => et.EquipmentTypeEntity)
                        .Include(c => c.Sessions)
                        .FirstOrDefaultAsync(c => c.CourseId == course.Id);

            if (entity is null) throw new NotFoundException("Course not found");

            var universityEntity = await _context.Universities.FindAsync(universityId);
            if (universityEntity == null) throw new NotFoundException("University not found!");
            var academicYearEntity = await _context.AcademicYears.FindAsync(academicYear);
            if (academicYearEntity == null) throw new NotFoundException("Academic Year not found!");

            entity.Name = course.Name;
            entity.Code = course.Code;
            entity.Description = course.Description;
            entity.TotalHours = course.TotalHours;
            entity.WeeklyHours = course.WeeklyHours;
            entity.Semester = course.Semester;
            entity.Credits = course.Credits;
            entity.UpdatedAt = DateTime.Now;

            if (course.EquipmentTypes is not null)
            {
                foreach (var equipmentTypeId in equipmentType)
                {
                    var equipmentEntity = await _context.EquipmentTypes.FindAsync(equipmentTypeId);
                    if (equipmentEntity == null) throw new NotFoundException("Equipment Type not found!");
                    entity.EquipmentTypes.Add(
                        new CourseEquipmentTypeEntity
                        {
                            AcademicYearEntity = academicYearEntity,
                            CourseEntity = entity,
                            EquipmentTypeEntity = equipmentEntity,
                            UniversityEntity = universityEntity,
                            Quantity = equipmentTypeId.Value
                        });
                }
            }

            await _context.SaveChangesAsync();
            return entity.ToDomainModel();
        }
    }
}
