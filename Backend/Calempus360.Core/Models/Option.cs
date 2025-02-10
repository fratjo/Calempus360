namespace Calempus360.Core.Models
{
    public class Option
    {
        public int Option_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Group> Groups { get; set; }
        public List<OptionCourse> OptionCourses { get; set; }
    }
}
