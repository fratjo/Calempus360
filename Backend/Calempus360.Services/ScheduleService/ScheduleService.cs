using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;
using Calempus360.Core.Interfaces.Schedule;

namespace Calempus360.Services.ScheduleService;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    
    public ScheduleService(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<GetGroupScheduleResponse> GetGroupScheduleAsync(GetGroupScheduleRequest request)
    {
        var respone = await _scheduleRepository.GetScheduleByGroupIdAsync(request.GroupId);

        return new GetGroupScheduleResponse() { };
    }
}