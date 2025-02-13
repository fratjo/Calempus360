using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class DayWithoutCourse
    {
        public Guid DayWithoutCourseId { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
    }
}
