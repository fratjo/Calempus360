using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Models.Models
{
    public class SiteCourseSchedule
    {
        public int AcademicYear_Id { get; set; }
        public int Site_Id { get; set; }
        public int Schedule_Id { get; set; }
        public CourseSchedule Schedule { get; set; }
        public Site Site { get; set; }
    }
}
