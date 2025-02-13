using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class CourseEquipmentType
    {
        // AcademicYear
        public string AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        // Course
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        
        // EquipmentType
        public Guid EquipmentTypeId { get; set; }
        public virtual EquipmentType EquipmentType { get; set; } = null!;
        
        // University
        public Guid UniversityId { get; set; }
        public virtual University University { get; set; } = null!;
        
        // Quantity
        public int Quantity { get; set; } = 1;
        

    }
}
