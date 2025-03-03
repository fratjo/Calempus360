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
        //TODO : Ajout Include dans Update et GetStudent
        public StudentGroupRepository(Calempus360DbContext context)
        {
            _context = context;
        }

        public async Task AddStudentGroupAsync(StudentGroup studentGroup)
        {
            var entity = studentGroup.ToEntity();
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

        public async Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync()
        {
            var entities = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .ToListAsync();

            return entities.Select(e => e.ToDomainModel());
        }

        public async Task<StudentGroup?> GetStudentGroupByIdAsync(Guid id)
        {
            var entity = await _context.StudentGroups
                .Include(sg => sg.SiteEntity)
                .Include(sg => sg.AcademicYearEntity)
                .Include(sg => sg.OptionEntity)
                .FirstOrDefaultAsync(sg => sg.StudentGroupId == id);
            return entity?.ToDomainModel();
        }

        public async Task<bool> UpdateStudentGroupAsync(StudentGroup studentGroup, Guid id)
        {
            var updatedEntity = studentGroup.ToEntity();
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
        //Trouver option selon nom
        public async Task<Option> GetOptionByName(string name)
        {
            var option = await _context.Options.FirstOrDefaultAsync(s => s.Name == name);
            return option.ToDomainModel();
        }
    }
}
