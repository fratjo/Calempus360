using Calempus360.Core.Models;

namespace Calempus360.Core.Models
{
    public class Site
    {
        public int Site_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int University_Id { get; set; }
        public University University { get; set; }
        public List<SiteAcademicYear> Sites_Academic_Year { get; set; }
        public List<Group> Groups { get; set; }
        public List<UniversitySiteEquipment> Equipments { get; set; }
        public List<SiteCourseSchedule> Schedules { get; set; }
        public List<Classroom> Classrooms { get; set; }
    }
}
