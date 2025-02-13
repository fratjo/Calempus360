namespace Calempus360.Core.Models
{
    public class Option
    {
        public Guid OptionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // StudentGroup
        public List<StudentGroup> StudentGroups { get; set; } 

        // OptionCourse
        public List<OptionCourse> OptionCourses { get; set; } = new();
    }
}
