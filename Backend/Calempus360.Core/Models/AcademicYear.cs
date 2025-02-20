namespace Calempus360.Core.Models;

public class AcademicYear
{
    public string                 Id                { get; private set; }
    public DateOnly               DateStart         { get; private set; }
    public DateOnly               DateEnd           { get; private set; }
    public DateTime               CreatedAt         { get; private set; }
    public DateTime               UpdatedAt         { get; private set; }
    public List<DayWithoutCourse> DaysWithoutCourse { get; private set; }
    
    public AcademicYear(
        string                 id,
        DateOnly               dateStart,
        DateOnly               dateEnd,
        DateTime               createdAt,
        DateTime               updatedAt,
        List<DayWithoutCourse> daysWithoutCourse)
    {
        Id                = id;
        DateStart         = dateStart;
        DateEnd           = dateEnd;
        CreatedAt         = createdAt;
        UpdatedAt         = updatedAt;
        DaysWithoutCourse = daysWithoutCourse;
    }
}