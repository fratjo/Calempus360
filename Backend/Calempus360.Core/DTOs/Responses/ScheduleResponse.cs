using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class ScheduleResponse
{
    
}

public static partial class DtoMapper
{
    public static ScheduleResponse MapToDTO(this Schedule schedule)
    {
        return new ScheduleResponse
        {
            
        };
    }
}