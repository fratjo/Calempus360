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
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteStudentGroupAsyncById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudentGroup>> GetAllStudentGroupAsync()
        {
            var entities = await _context.StudentGroups.ToListAsync();

            return entities.Select(e => e.ToDomainModel());
        }

        public async Task<StudentGroup?> GetStudentGroupAsyncById(Guid id)
        {
            var entities = await _context.StudentGroups.FindAsync(id);
            return entities?.ToDomainModel();
        }

        public async Task<bool> UpdateStudentGroupAsync(StudentGroup studentGroup)
        {
            throw new NotImplementedException();
        }
    }
}
