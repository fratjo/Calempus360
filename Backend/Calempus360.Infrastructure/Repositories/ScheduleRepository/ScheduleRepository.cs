using Calempus360.Core.Interfaces.Schedule;
using Calempus360.Core.Models;
using Calempus360.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Calempus360.Infrastructure.Repositories.ScheduleRepository;

public class ScheduleRepository : IScheduleRepository
{
    private readonly Calempus360DbContext _context;
    
    public async Task<Session> GetScheduleByGroupIdAsync(int groupId)
    {
        await Task.Delay(1);
        throw new NotImplementedException("Not implemented yet");
        
        /* use _context eg */
        
        var schedule = await _context.StudentGroups
                                     .Include(sg => sg.StudentGroupSessions)
                                     .ThenInclude(s => s.Session)
                                     .Where(sg => sg.Code == groupId.ToString())
                                     .FirstOrDefaultAsync();
    }
}