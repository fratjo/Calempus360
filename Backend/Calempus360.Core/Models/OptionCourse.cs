using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class OptionCourse
    {
        public int AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        
        public int OptionId { get; set; }
        public virtual Option Option { get; set; } = null!;
        
        public int OptionGrade { get; set; }
    }
}
