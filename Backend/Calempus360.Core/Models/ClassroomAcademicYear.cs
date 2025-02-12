using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class ClassroomAcademicYear
    {
        // AcademicYear
        public int AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        // Classroom
        public int ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; } = null!;
    }
}
