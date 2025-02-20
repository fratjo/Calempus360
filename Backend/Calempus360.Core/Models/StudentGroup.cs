namespace Calempus360.Core.Models;

public class StudentGroup
{
    public Guid          Id               { get; private set; }
    public string        Code             { get; private set; }
    public int           NumberOfStudents { get; private set; }
    public int           OptionGrade      { get; private set; }
    public DateTime      CreatedAt        { get; private set; }
    public DateTime      UpdatedAt        { get; private set; }
    public Site          Site             { get; private set; }
    public List<Session> Sessions         { get; private set; }
    public Option        Option           { get; private set; }
    
    public StudentGroup(
        Guid     id,
        string   code,
        int      numberOfStudents,
        int      optionGrade,
        DateTime createdAt,
        DateTime updatedAt,
        Site     site,
        List<Session> sessions,
        Option   option)
    {
        Id               = id;
        Code             = code;
        NumberOfStudents = numberOfStudents;
        OptionGrade      = optionGrade;
        CreatedAt        = createdAt;
        UpdatedAt        = updatedAt;
        Site             = site;
        Sessions         = sessions;
        Option           = option;
    }
}