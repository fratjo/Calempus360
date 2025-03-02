namespace Calempus360.Core.Models;

public class AcademicYear(
    string                  id,
    DateOnly                dateStart,
    DateOnly                dateEnd,
    DateTime?               createdAt          = null,
    DateTime?               updatedAt          = null,
    List<DayWithoutCourse>? daysWithoutCourses = null)
{
    public string                  Id                { get; private set; } = id;
    public DateOnly                DateStart         { get; private set; } = dateStart;
    public DateOnly                DateEnd           { get; private set; } = dateEnd;
    public DateTime                CreatedAt         { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime                UpdatedAt         { get; private set; } = updatedAt ?? DateTime.Now;
    public List<DayWithoutCourse>? DaysWithoutCourses { get; private set; } = daysWithoutCourses;
}