using Calempus360.Core.Interfaces.Option;
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
    public class OptionRepository : IOptionRepository
    {
        private readonly Calempus360DbContext _context;

        public OptionRepository(Calempus360DbContext context)
        {
            _context = context;
        }

        public async Task<Option> AddOptionAsync(Option option, Guid academicYear)
        {
            var academicYearEntity = await _context.AcademicYears.FindAsync(academicYear);
            if(academicYearEntity == null) throw new NotFoundException("Academic Year not found !");
            var entity = option.ToEntity();

            await _context.Options.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ToDomainModel();          
        }

        public async Task<bool> DeleteOptionAsync(Guid id)
        {
            var entity = await _context.Options
                        .Include(o => o.OptionCourses)
                            .ThenInclude(oc => oc.CourseEntity)
                        .Include(o => o.OptionCourses)
                            .ThenInclude(oc => oc.AcademicYearEntity)
                        .Include(o => o.StudentGroups)
                        .FirstOrDefaultAsync(o => o.OptionId == id);
            if (entity == null) throw new NotFoundException("Option not found");

            //Clear les relations présents dans la table de liaison
            entity.OptionCourses.RemoveAll(oc => oc.OptionId == id);

            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Option>> GetAllOptionAsync()
        {
            var entities = await _context.Options
                .Include(o => o.OptionCourses)
                    .ThenInclude(oc => oc.CourseEntity)
                .Include(o => o.OptionCourses)
                    .ThenInclude(oc => oc.AcademicYearEntity)
                .Include(o => o.StudentGroups)
                .ToListAsync();

            return entities.Select(e => e.ToDomainModel());
        }

        public async Task<Option?> GetOptionByIdAsync(Guid id)
        {
            var entity = await _context.Options
                .Include(o => o.OptionCourses)
                    .ThenInclude(oc => oc.CourseEntity)
                .Include(o => o.OptionCourses)
                    .ThenInclude(oc => oc.AcademicYearEntity)
                .Include(o => o.StudentGroups)
                .FirstOrDefaultAsync(o => o.OptionId == id);
            if (entity == null) throw new NotFoundException("Option not found !");
            return entity.ToDomainModel();
        }

        public async Task<Option> UpdateOptionAsync(Option option, Guid academicYear)
        {
            var entity = await _context.Options
                        .Include(o => o.OptionCourses)
                            .ThenInclude(oc => oc.CourseEntity)
                        .Include(o => o.OptionCourses)
                            .ThenInclude(oc => oc.AcademicYearEntity)
                        .Include(o => o.StudentGroups)
                        .FirstOrDefaultAsync(o => o.OptionId == option.Id);
            if (entity == null) throw new NotFoundException("Option not found");

            var academicYearEntity = await _context.AcademicYears.FindAsync(academicYear);
            if (academicYearEntity == null) throw new NotFoundException("Academic Year not found !");

            entity.Name = option.Name;
            entity.Code = option.Code;
            entity.Description = option.Description;
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return entity.ToDomainModel();
        }
    }
}
