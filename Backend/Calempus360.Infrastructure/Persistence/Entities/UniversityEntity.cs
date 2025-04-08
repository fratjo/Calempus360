namespace Calempus360.Infrastructure.Persistence.Entities;

public class UniversityEntity
{
    public Guid?     UniversityId { get; set; }
    public string   Name         { get; set; }
    public string   Code         { get; set; }
    public string   Phone        { get; set; }
    public string   Address      { get; set; }
    public DateTime? CreatedAt    { get; set; }
    public DateTime? UpdatedAt    { get; set; }

    // Navigation Properties

    // Site
    public virtual List<SiteEntity> Sites { get; set; } = new();

    // UniversitySiteEquipment
    public virtual List<UniversitySiteEquipmentEntity> Equipments { get; set; }

    // CourseEquipmentType
    public virtual List<CourseEquipmentTypeEntity> CourseEquipmentTypes { get; set; }
}