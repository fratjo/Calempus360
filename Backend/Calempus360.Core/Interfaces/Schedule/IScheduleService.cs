using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.DTOs.Responses;

namespace Calempus360.Core.Interfaces.Schedule;

public interface IScheduleService
{
    Task<GetGroupScheduleResponse> GetGroupScheduleAsync(GetGroupScheduleRequest request);
}