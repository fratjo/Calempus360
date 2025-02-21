namespace Calempus360.Core.Models;

public class StudentGroup(
    Guid     id,
    string   code,
    int      numberOfStudents,
    int      optionGrade,
    DateTime createdAt,
    DateTime updatedAt,
    Site     site,
    Option   option)
{
    public Guid     Id               { get; private set; } = id;
    public string   Code             { get; private set; } = code;
    public int      NumberOfStudents { get; private set; } = numberOfStudents;
    public int      OptionGrade      { get; private set; } = optionGrade;
    public DateTime CreatedAt        { get; private set; } = createdAt;
    public DateTime UpdatedAt        { get; private set; } = updatedAt;
    public Site     Site             { get; private set; } = site;
    public Option   Option           { get; private set; } = option;
}