using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class UniversitySiteEquipment
    {
        // AcademicYear
        public int AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; } = null!;
        
        // Equipment
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;
        
        // Site
        public int SiteId { get; set; }
        public Site Site { get; set; }
        
        // University
        public int UniversityId { get; set; }
        public University University { get; set; } = null!;
    }
}
