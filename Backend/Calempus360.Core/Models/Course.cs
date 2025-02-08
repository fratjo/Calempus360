namespace Calempus360.Models.Models
{
    public class Course
    {
        public string Course_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int TotalHours { get; set; }
        public int WeeklyHours { get; set; }
        public string Semester { get; set; }
        public int Credits { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<OptionCourse> OptionsCourse { get; set; }
    }
}
