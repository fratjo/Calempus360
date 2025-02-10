
namespace Calempus360.Core.Models;

public class Group
{
    public int Group_Id { get; set; }
    public string Code { get; set; }
    public int NumberOfStudents { get; set; }
    public int OptionGrade { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int AcademicYear_Id { get; set; }
    public int Option_Id { get; set; }
    public int Site_Id { get; set; }
    public Site MainSite { get; set; }
    public Option Option { get; set; }

}