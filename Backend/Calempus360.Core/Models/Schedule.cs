namespace Calempus360.Core.Models;

public enum DayOfWeek : int
{
    Sunday    = 0,
    Monday    = 1,
    Tuesday   = 2,
    Wednesday = 3,
    Thursday  = 4,
    Friday    = 5,
    Saturday  = 6
}

public class Schedule(
    DayOfWeek dayOfWeek,
    TimeOnly  timeStart,
    TimeOnly  timeEnd,
    Guid?      id = null)
{
    public Guid      Id        { get; private set; } = id ?? Guid.NewGuid();
    public DayOfWeek DayOfWeek { get; private set; } = dayOfWeek;
    public TimeOnly  TimeStart { get; private set; } = timeStart;
    public TimeOnly  TimeEnd   { get; private set; } = timeEnd;
}