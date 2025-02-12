using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class SiteCourseSchedule
    {
        public int AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        public int SiteId { get; set; }
        public virtual Site Site { get; set; } = null!;
        
        public int ScheduleId { get; set; }
        public virtual CourseSchedule CourseSchedule { get; set; } = null!;
    }
}
