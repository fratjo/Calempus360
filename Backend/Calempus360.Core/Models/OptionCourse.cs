using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class OptionCourse
    {   
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        // Course
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        
        // Option
        public Guid OptionId { get; set; }
        public virtual Option Option { get; set; } = null!;
        
        // OptionGrade
        public int OptionGrade { get; set; }
    }
}
