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

public class Schedule
{
    public Guid      Id        { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSpan  TimeStart { get; private set; }
    public TimeSpan  TimeEnd   { get; private set; }
    
    public Schedule(
        Guid      id,
        DayOfWeek dayOfWeek,
        TimeSpan  timeStart,
        TimeSpan  timeEnd)
    {
        Id        = id;
        DayOfWeek = dayOfWeek;
        TimeStart = timeStart;
        TimeEnd   = timeEnd;
    }
}