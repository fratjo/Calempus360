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

        public async Task AddStudentGroupAsync(StudentGroup studentGroup)
        {
            var entity = studentGroup.ToEntity();
            await _context.StudentGroups.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteStudentGroupAsyncById(Guid id)
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
            var entities = await _context.StudentGroups.ToListAsync();

            return entities.Select(e => e.ToDomainModel());
        }

        public async Task<StudentGroup?> GetStudentGroupAsyncById(Guid id)
        {
            var entity = await _context.StudentGroups.FindAsync(id);
            return entity?.ToDomainModel();
        }

        public async Task<bool> UpdateStudentGroupAsync(StudentGroup studentGroup)
        {
            var updatedEntity = studentGroup.ToEntity();
            var entity = await _context.StudentGroups.FindAsync(updatedEntity.StudentGroupId);
            if(entity != null)
            {
                entity = updatedEntity;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
