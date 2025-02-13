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
        public string AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        // Equipment
        public Guid EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; } = null!;
        
        // Classroom
        public Guid ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; } = null!;
    }
}
