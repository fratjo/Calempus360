namespace Calempus360.Infrastructure.Persistence.Entities;

public class StudentGroupEntity
{
    public Guid StudentGroupId { get; set; }
    public string Code { get; set; }
    public int NumberOfStudents { get; set; }
    public int OptionGrade { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation Properties
    
    // AcademicYear
    public Guid? AcademicYearId { get; set; }
    public virtual AcademicYearEntity? AcademicYearEntity { get; set; } = null!;
    
    // Site
    public Guid? SiteId { get; set; }
    public virtual SiteEntity? SiteEntity { get; set; } = null!;
    
    // Option
    public Guid? OptionId { get; set; }
    public virtual OptionEntity? OptionEntity { get; set; } = null!;
    
    // StudentGroupSessions
    public virtual List<StudentGroupSessionEntity> StudentGroupSessions { get; set; } 

}