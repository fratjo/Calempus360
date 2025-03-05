using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class ScheduleResponseDto
{
    
}

public static partial class DtoMapper
{
    public static ScheduleResponseDto MapToDTO(this Schedule schedule)
    {
        return new ScheduleResponseDto
        {
            
        };
    }
}