using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class CourseEquipmentType
    {
        public int AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        
        public int EquipmentTypeId { get; set; }
        public virtual EquipmentType EquipmentType { get; set; } = null!;
        
        public int UniversityId { get; set; }
        public virtual University University { get; set; } = null!;
        
        public int Quantity { get; set; } = 1;
        

    }
}
