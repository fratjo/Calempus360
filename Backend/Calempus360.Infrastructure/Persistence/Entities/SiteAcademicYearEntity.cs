namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class SiteAcademicYearEntity
    {   
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYearEntity AcademicYearEntity { get; set; } = null!;
        
        // Site
        public Guid SiteId { get; set; }
        public virtual SiteEntity SiteEntity { get; set; } = null!;
    }
}
