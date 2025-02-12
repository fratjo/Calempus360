using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class ClassroomEquipment
    {
        // AcademicYear
        public int AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        // Equipment
        public int EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; } = null!;
        
        // Classroom
        public int ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; } = null!;
    }
}
