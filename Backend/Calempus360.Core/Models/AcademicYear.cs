using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class AcademicYear
    {
        public string AcademicYearId { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // SiteAcademicYear
        public virtual List<SiteAcademicYear> SiteAcademicYears { get; set; }
        
        // SiteCourseSchedule
        public virtual List<SiteCourseSchedule> SiteCourseSchedules { get; set; }
        
        // ClassroomAcademicYear
        public virtual List<ClassroomAcademicYear> ClassroomAcademicYears { get; set; }
        
        // StudentGroup
        public virtual List<StudentGroup> StudentGroups { get; set; }
        
        // ClassroomEquipment
        public virtual List<ClassroomEquipment> ClassroomEquipments { get; set; }
        
        // UniversitySiteEquipment
        public virtual List<UniversitySiteEquipment> UniversitySiteEquipments { get; set; }
        
        // CourseEquipmentType
        public virtual List<CourseEquipmentType> CourseEquipmentTypes { get; set; }
        
        // OptionCourse
        public virtual List<OptionCourse> OptionCourses { get; set; }
        
        // DayWithoutCourse
        public virtual List<DayWithoutCourse> DaysWithoutCourse { get; set; } = new();
    }
}
