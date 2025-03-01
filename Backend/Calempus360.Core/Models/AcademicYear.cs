namespace Calempus360.Core.Models;

public class AcademicYear(
    string                  id,
    DateOnly                dateStart,
    DateOnly                dateEnd,
    DateTime?                createdAt,
    DateTime?                updatedAt,
    List<DayWithoutCourse>? daysWithoutCourses)
{
    public string                  Id                { get; private set; } = id;
    public DateOnly                DateStart         { get; private set; } = dateStart;
    public DateOnly                DateEnd           { get; private set; } = dateEnd;
    public DateTime?                CreatedAt         { get; private set; } = createdAt;
    public DateTime?                UpdatedAt         { get; private set; } = updatedAt;
    public List<DayWithoutCourse>? DaysWithoutCourses { get; private set; } = daysWithoutCourses;
}