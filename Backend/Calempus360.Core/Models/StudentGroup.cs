
namespace Calempus360.Core.Models;

public class StudentGroup
{
    public Guid GroupId { get; set; }
    public string Code { get; set; }
    public int NumberOfStudents { get; set; }
    public int OptionGrade { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation Properties
    
    // AcademicYear
    public string AcademicYearId { get; set; }
    public virtual AcademicYear AcademicYear { get; set; } = null!;
    
    // Site
    public Guid SiteId { get; set; }
    public virtual Site Site { get; set; } = null!;
    
    // Option
    public Guid OptionId { get; set; }
    public virtual Option Option { get; set; } = null!;
    
    // StudentGroupSessions
    public virtual List<StudentGroupSession> StudentGroupSessions { get; set; } 

}