using Calempus360.Core.Interfaces.Group;
using Calempus360.Core.Models;
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

        public async Task AddStudentGroupAsync(StudentGroup studentGroup, string academicYear)
        {
            var siteEntity = await _context.Sites.FindAsync(studentGroup.Site.Id);
            var optionEntity = await _context.Options.FindAsync(studentGroup.Option.Id);
            var academicYearEntity = await _context.AcademicYears.FindAsync(academicYear);
            var entity = studentGroup.ToEntity();

            entity.SiteEntity = siteEntity!;
            entity.OptionEntity = optionEntity!;
            entity.AcademicYearEntity = academicYearEntity!;

            await _context.StudentGroups.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteStudentGroupByIdAsync(Guid id)
        {
            var entity = await _context.StudentGroups.FindAsync(id);
            if(entity != null)
        {
                _context.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync(string academicYear)
        {
            var entities = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .Where(sg => sg.AcademicYearId == academicYear)
                .ToListAsync();

            return entities.Select(e => e.ToDomainModel());
        }

        public async Task<StudentGroup?> GetStudentGroupByIdAsync(Guid id)
        {
            var entity = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .FirstOrDefaultAsync(sg => sg.StudentGroupId == id && sg.AcademicYearId == academicYear);
            return entity?.ToDomainModel();
        }

        public async Task<bool> UpdateStudentGroupAsync(StudentGroup studentGroup)
        {
            var entity = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .FirstOrDefaultAsync(sg => sg.StudentGroupId == studentGroup.Id);
            if(entity != null)
            {
                entity.Code = studentGroup.Code;
                entity.NumberOfStudents = studentGroup.NumberOfStudents;
                entity.OptionGrade = studentGroup.OptionGrade;
                entity.SiteId = studentGroup.Site.Id;
                entity.OptionId = studentGroup.Option.Id;
                entity.UpdatedAt = DateTime.Now;

                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        //----------Méthode pour certains tests

        //Trouver site selon nom
        public async Task<Site> GetSiteByName(string name)
        {
            var site = await _context.Sites.FirstOrDefaultAsync(s => s.Name == name);
            return site.ToDomainModel();
        }
    }
}
