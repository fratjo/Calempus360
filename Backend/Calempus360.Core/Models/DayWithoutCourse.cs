namespace Calempus360.Core.Models;

public class DayWithoutCourse(
    Guid     id,
    string   name,
    DateOnly date,
    DateTime createdAt,
    DateTime updatedAt)
{
    public Guid     Id        { get; private set; } = id;
    public string   Name      { get; private set; } = name;
    public DateOnly Date      { get; private set; } = date;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime UpdatedAt { get; private set; } = updatedAt;
}