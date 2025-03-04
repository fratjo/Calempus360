using Calempus360.Core.Interfaces.IOption;
using Calempus360.Core.Models;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
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

        public async Task AddOptionAsync(Option option)
        {
            //List<CourseEntity> courses = new List<CourseEntity>();
            //foreach(Course course in option.Courses)
            //{
            //    var courseEntity = await _context.Courses.FindAsync(course.Id);
            //    if(courseEntity != null) courses.Add(courseEntity);
            //}
            var optionEntity = option.ToEntity();
            
            await _context.Options.AddAsync(optionEntity);
            await _context.SaveChangesAsync();
        }

        public Task<bool> DeleteOptionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Option>> GetAllOptionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Option?> GetOptionByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOptionAsync(Option option)
        {
            throw new NotImplementedException();
        }
    }
}
