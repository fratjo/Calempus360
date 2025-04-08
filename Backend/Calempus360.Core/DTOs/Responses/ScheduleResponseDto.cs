using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class ScheduleResponseDto
{
    public Guid? Id { get; set; }
    public Models.DayOfWeek? DayOfWeek { get; set; }
    public string? TimeStart { get; set; }
    public string? TimeEnd { get; set; }
}

public static partial class DtoMapper
{
    public static ScheduleResponseDto MapToDto(this Schedule schedule)
    {
        return new ScheduleResponseDto
        {
            Id = schedule.Id,
            DayOfWeek = schedule.DayOfWeek,
            TimeStart = schedule.TimeStart.ToString(),
            TimeEnd = schedule.TimeEnd.ToString()
        };
    }
}