using Calempus360.Core.Interfaces.DayWithoutCourse;
using Calempus360.Core.Models;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Infrastructure.Repositories
{
    public class DayWithoutCourseRepository : IDayWithoutCourseRepository
    {
        private readonly Calempus360DbContext _context;

        public DayWithoutCourseRepository(Calempus360DbContext context)
        {
            _context = context;
        }

        public async Task<DayWithoutCourse> AddDayWithoutCourseAsync(DayWithoutCourse dayWithoutCourse, Guid academicYear)
        {
            var academicYearEntity = await _context.AcademicYears.FindAsync(academicYear);
            if (academicYearEntity == null) throw new NotFoundException("academic Year not found !");
            var entity = dayWithoutCourse.ToEntity();
            entity.AcademicYearId = academicYear;
            await _context.DaysWithoutCourse.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ToDomainModel();  
        }

        public async Task<bool> DeleteDayWithoutCourseAsync(Guid id)
        {
            var entity = await _context.DaysWithoutCourse.FindAsync(id);
            if (entity == null) throw new NotFoundException("Non Working Day not found !");
            _context.DaysWithoutCourse.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DayWithoutCourse>> GetAllDayWithoutCourseAsync(Guid academicYear)
        {
            var entities = await _context.DaysWithoutCourse
                                .Include(a => a.AcademicYearEntity)
                                .Where(dwc => dwc.AcademicYearId == academicYear)
                                .ToListAsync();
            return entities.Select(dwc => dwc.ToDomainModel()).ToList();
            
        }

        public async Task<DayWithoutCourse> GetDayWithoutCourseByIdAsync(Guid id)
        {
            var entity = await _context.DaysWithoutCourse
                                .Include(a => a.AcademicYearEntity)
                                .FirstOrDefaultAsync(dwc => dwc.DayWithoutCourseId == id);
            if (entity == null) throw new NotFoundException("Non Working Day not found !");
            return entity.ToDomainModel();
        }

        public async Task<DayWithoutCourse> UpdateDayWithoutCourseAsync(DayWithoutCourse dayWithoutCourse)
        {
            var entity = await _context.DaysWithoutCourse
                                .Include(a => a.AcademicYearEntity)
                                .FirstOrDefaultAsync(dwc => dwc.DayWithoutCourseId == dayWithoutCourse.Id);
            if (entity == null) throw new NotFoundException("Non Working Day not found !");

            entity.Name = dayWithoutCourse.Name;
            entity.Date = dayWithoutCourse.Date;
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return entity.ToDomainModel();
        }
    }
}
