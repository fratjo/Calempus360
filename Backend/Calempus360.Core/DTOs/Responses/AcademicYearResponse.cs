using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class AcademicYearResponse
{
    public string Id { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<DayWithoutCourseResponse>? DaysWithoutCourses { get; set; }
}

public static partial class DtoMapper
{
    public static AcademicYearResponse MapToDto(this AcademicYear academicYear)
    {
        return new AcademicYearResponse
        {
            Id                = academicYear.Id,
            DateStart         = academicYear.DateStart,
            DateEnd           = academicYear.DateEnd,
            CreatedAt         = academicYear.CreatedAt,
            UpdatedAt         = academicYear.UpdatedAt,
            DaysWithoutCourses = academicYear.DaysWithoutCourses?.Select(dayWithoutCourse => dayWithoutCourse.MapToDto()).ToList()
        };
    } 
}