namespace Calempus360.Infrastructure.Persistence.Entities
{
    public class OptionEntity
    {
        public Guid OptionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties

        // StudentGroup
        public List<StudentGroupEntity> StudentGroups { get; set; } = new();

        // OptionCourse
        public List<OptionCourseEntity> OptionCourses { get; set; } = new();
    }
}
