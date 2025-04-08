namespace Calempus360.Core.Models;

public class StudentGroup(
    string    code,
    int       numberOfStudents,
    int       optionGrade,
    Guid?     id = null,
    DateTime? createdAt = null,
    DateTime? updatedAt = null,
    Site?     site = null,
    Option?   option = null)
{
    public Guid     Id               { get; private set; } = id ?? Guid.NewGuid();
    public string   Code             { get; private set; } = code;
    public int      NumberOfStudents { get; private set; } = numberOfStudents;
    public int      OptionGrade      { get; private set; } = optionGrade;
    public DateTime CreatedAt        { get; private set; } = createdAt ?? DateTime.Now;
    public DateTime UpdatedAt        { get; private set; } = updatedAt ?? DateTime.Now;
    public Site?    Site             { get; private set; } = site;
    public Option?   Option           { get; private set; } = option;
}