namespace Calempus360.Core.Models;

public class DayWithoutCourse
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateOnly Date { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public DayWithoutCourse(
        Guid     id,
        string   name,
        DateOnly date,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id        = id;
        Name      = name;
        Date      = date;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}