using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Models;
using Calempus360.Errors;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Calempus360.Infrastructure.Repositories;

public class SitesRepository(Calempus360DbContext dbContext) : ISiteRepository
{
    public async Task<IEnumerable<Site>> GetSitesAsync()
    {
        var sites = await dbContext.Sites
                                   .Include(s => s.SiteCourseSchedules)
                                        .ThenInclude(scs => scs.CourseScheduleEntity)
                                   .Include(s => s.Classrooms)
                                   .Include(s => s.Equipments)
                                   .ToListAsync();

        return sites.Select(s => s.ToDomainModel());
    }

    public async Task<IEnumerable<Site?>> GetSitesByUniversityAsync(Guid universityId)
    {
        var sites = from s in await dbContext.Sites
                             .Include(s => s.SiteCourseSchedules)
                                .ThenInclude(scs => scs.CourseScheduleEntity)
                             .Include(s => s.Classrooms)
                             .Include(s => s.Equipments)
                             .ToListAsync()
                    where s.UniversityId == universityId
                    select s;

        return sites.Select(s => s?.ToDomainModel());
    }

    public async Task<Site> GetSiteByIdAsync(Guid id)
    {
        var site = await dbContext.Sites
                                  .Include(s => s.SiteCourseSchedules)
                                    .ThenInclude(scs => scs.CourseScheduleEntity)
                                  .Include(s => s.Classrooms)
                                  .Include(s => s.Equipments)
                                  .FirstOrDefaultAsync(s => s.SiteId == id);

        if (site == null) throw new NotFoundException("Site not found");

        return site.ToDomainModel();
    }

    public async Task<Site> CreateSiteAsync(Site site, Guid universityId)
    {
        var entity = site.ToEntity();

        entity.UniversityId = universityId;

        // opening schedule

        // for each schedule
        site.Schedules?.ForEach(schedule =>
        {
            var existingSchedule = dbContext.CoursesSchedules.FirstOrDefault(s =>
                                                // if both schedules are in the same day and time
                                                s.HourStart == schedule.TimeStart
                                                && s.HourEnd == schedule.TimeEnd
                                                && s.DayOfTheWeek == (int)schedule.DayOfWeek);

            if (existingSchedule != null) entity.SiteCourseSchedules.Add(new SiteCourseScheduleEntity
            {
                SiteId = (Guid)entity.SiteId!,
                ScheduleId = existingSchedule.ScheduleId
            });
            else
            {
                var newSchedule = new CourseScheduleEntity
                {
                    ScheduleId = Guid.NewGuid(),
                    DayOfTheWeek = (int)schedule.DayOfWeek,
                    HourStart = schedule.TimeStart,
                    HourEnd = schedule.TimeEnd,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                dbContext.CoursesSchedules.Add(newSchedule);
                dbContext.SaveChanges();

                entity.SiteCourseSchedules.Add(new SiteCourseScheduleEntity
                {
                    SiteId = (Guid)entity.SiteId!,
                    ScheduleId = newSchedule.ScheduleId
                });
            }
        });

        await dbContext.Sites.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<Site> UpdateSiteAsync(Site site)
    {
        var entity = await dbContext.Sites.FirstOrDefaultAsync(s => s.SiteId == site.Id);

        if (entity == null) throw new NotFoundException("Site not found");

        entity.Name = site.Name;
        entity.Code = site.Code;
        entity.Address = site.Address;
        entity.Phone = site.Phone;
        entity.UpdatedAt = site.UpdatedAt;

        // for each schedule
        site.Schedules?.ForEach(schedule =>
        {
            var existingSchedule = dbContext.CoursesSchedules.FirstOrDefault(s =>
                                                // if both schedules are in the same day
                                                s.DayOfTheWeek == (int)schedule.DayOfWeek &&
                                                    (
                                                        // if both schedules are in the morning
                                                        (s.HourEnd.ToTimeSpan() <= new TimeSpan(12, 0, 0)
                                                        && schedule.TimeEnd.ToTimeSpan() <= new TimeSpan(12, 0, 0))
                                                    ||
                                                        // if both schedules are in the afternoon
                                                        (s.HourStart.ToTimeSpan() >= new TimeSpan(12, 0, 0)
                                                        && schedule.TimeStart.ToTimeSpan() >= new TimeSpan(12, 0, 0))));

            if (existingSchedule != null) entity.SiteCourseSchedules.Add(new SiteCourseScheduleEntity
            {
                SiteId = (Guid)entity.SiteId!,
                ScheduleId = existingSchedule.ScheduleId
            });
            else
            {
                var newSchedule = new CourseScheduleEntity
                {
                    ScheduleId = Guid.NewGuid(),
                    DayOfTheWeek = (int)schedule.DayOfWeek,
                    HourStart = schedule.TimeStart,
                    HourEnd = schedule.TimeEnd,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                dbContext.CoursesSchedules.Add(newSchedule);
                dbContext.SaveChanges();

                entity.SiteCourseSchedules.Add(new SiteCourseScheduleEntity
                {
                    SiteId = (Guid)entity.SiteId!,
                    ScheduleId = newSchedule.ScheduleId
                });
            }
        });

        await dbContext.SaveChangesAsync();

        return entity.ToDomainModel();
    }

    public async Task<bool> DeleteSiteAsync(Guid id)
    {
        var entity = await dbContext.Sites.FirstOrDefaultAsync(s => s.SiteId == id);

        if (entity == null) throw new NotFoundException("Site not found");

        dbContext.Sites.Remove(entity);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteSitesByUniversityAsync(Guid universityId)
    {
        var sites = await dbContext.Sites.Where(s => s.UniversityId == universityId).ToListAsync();

        dbContext.Sites.RemoveRange(sites);

        await dbContext.SaveChangesAsync();

        return true;
    }
}