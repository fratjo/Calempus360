namespace Calempus360.Core.Models;

public class AcademicYear(
    string                  code,
    DateOnly                dateStart,
    DateOnly                dateEnd,
    Guid?                   id                = null,
    DateTime?               createdAt          = null,
    DateTime?               updatedAt          = null,
    List<DayWithoutCourse>? daysWithoutCourses = null)
{
    public Guid?                    Id                { get; private set; } = id ?? Guid.NewGuid();
    public string                  Code                { get; private set; } = code;
    public DateOnly                DateStart         { get; private set; } = dateStart;
    public DateOnly                DateEnd           { get; private set; } = dateEnd;
    public DateTime                CreatedAt         { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime                UpdatedAt         { get; private set; } = updatedAt ?? DateTime.Now;
    public List<DayWithoutCourse>? DaysWithoutCourses { get; private set; } = daysWithoutCourses;
}