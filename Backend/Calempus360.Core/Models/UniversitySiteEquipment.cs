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
        public string AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; } = null!;
        
        // Equipment
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; } = null!;
        
        // Site
        public Guid SiteId { get; set; }
        public Site Site { get; set; }
        
        // University
        public Guid UniversityId { get; set; }
        public University University { get; set; } = null!;
    }
}
