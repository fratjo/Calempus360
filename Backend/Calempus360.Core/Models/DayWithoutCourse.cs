namespace Calempus360.Core.Models;

public class DayWithoutCourse(
    string    name,
    DateOnly  date,
    Guid?     id        = null,
    DateTime? createdAt = null,
    DateTime? updatedAt = null)
{
    public Guid     Id        { get; private set; } = id ?? Guid.NewGuid();
    public string   Name      { get; private set; } = name;
    public DateOnly Date      { get; private set; } = date;
    public DateTime CreatedAt { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime UpdatedAt { get; private set; } = updatedAt ?? DateTime.Now;
}