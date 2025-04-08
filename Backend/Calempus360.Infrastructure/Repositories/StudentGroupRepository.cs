using Calempus360.Core.Interfaces.Group;
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
    public class StudentGroupRepository : IStudentGroupRepository
    {
        private readonly Calempus360DbContext _context;

        public StudentGroupRepository(Calempus360DbContext context)
        {
            _context = context;
        }

        public async Task<StudentGroup> AddStudentGroupAsync(StudentGroup studentGroup, Guid academicYear, Guid option, Guid site)
        {
            var siteEntity = await _context.Sites.FindAsync(site);
            var optionEntity = await _context.Options.FindAsync(option);
            var academicYearEntity = await _context.AcademicYears.FindAsync(academicYear);
            var entity = studentGroup.ToEntity();

            if(siteEntity == null) throw new NotFoundException("Site not found");
            else entity.SiteEntity = siteEntity;
            if (optionEntity == null) throw new NotFoundException("Option not found");
            else entity.OptionEntity = optionEntity;
            if (academicYearEntity == null) throw new NotFoundException("Academic Year not found");
            else entity.AcademicYearEntity = academicYearEntity;

            await _context.StudentGroups.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ToDomainModel();
        }

        public async Task<bool> DeleteStudentGroupByIdAsync(Guid id)
        {
            var entity = await _context.StudentGroups.FindAsync(id);
            if (entity == null) throw new NotFoundException("Student Group not found");
            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync(Guid academicYear, Guid universityId)
        {
            var sitesEntity = await _context.Sites.Where(s => s.UniversityId == universityId).ToListAsync();
            if (sitesEntity == null) throw new NotFoundException("This university has no sites");
            var entities = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .Where(sg => sg.AcademicYearId == academicYear && sitesEntity.Contains(sg.SiteEntity!))
                .ToListAsync();

            return entities.Select(e => e.ToDomainModel());
        }

        public async Task<StudentGroup> GetStudentGroupByIdAsync(Guid id, Guid academicYear)
        {
            var entity = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .FirstOrDefaultAsync(sg => sg.StudentGroupId == id && sg.AcademicYearId == academicYear);
            if(entity == null) throw new NotFoundException("Student Group not found");
            return entity.ToDomainModel();
        }

        public async Task<StudentGroup> UpdateStudentGroupAsync(StudentGroup studentGroup, Guid option, Guid site)
        {
            var entity = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .FirstOrDefaultAsync(sg => sg.StudentGroupId == studentGroup.Id);

            if (entity == null) throw new NotFoundException("Student Group not found");

            var siteEntity = await _context.Sites.FindAsync(site);
            var optionEntity = await _context.Options.FindAsync(option);

            if (siteEntity == null) throw new NotFoundException("Site not found");
            if (optionEntity == null) throw new NotFoundException("Option not found");
        
            entity.Code = studentGroup.Code;
            entity.NumberOfStudents = studentGroup.NumberOfStudents;
            entity.OptionGrade = studentGroup.OptionGrade;
            entity.SiteId = site;
            entity.OptionId = option;
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return entity.ToDomainModel();
            
        }
    }
}
