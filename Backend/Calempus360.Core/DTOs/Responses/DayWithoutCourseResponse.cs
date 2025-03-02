using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class DayWithoutCourseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public static partial class DtoMapper
{
    public static DayWithoutCourseResponse MapToDto(this DayWithoutCourse dayWithoutCourse)
    {
        return new DayWithoutCourseResponse
        {
            Id        = dayWithoutCourse.Id,
            Name      = dayWithoutCourse.Name,
            Date      = dayWithoutCourse.Date,
            CreatedAt = dayWithoutCourse.CreatedAt,
            UpdatedAt = dayWithoutCourse.UpdatedAt
        };
    }
}