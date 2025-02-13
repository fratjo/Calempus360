using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class SiteCourseSchedule
    {
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        // Site
        public Guid SiteId { get; set; }
        public virtual Site Site { get; set; } = null!;
        
        // CourseSchedule
        public Guid ScheduleId { get; set; }
        public virtual CourseSchedule CourseSchedule { get; set; } = null!;
    }
}
