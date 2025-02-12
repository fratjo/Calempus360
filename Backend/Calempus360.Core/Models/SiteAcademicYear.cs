using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class SiteAcademicYear
    {   
        // AcademicYear
        public int AcademicYearId { get; set; }
        public virtual AcademicYear AcademicYear { get; set; } = null!;
        
        // Site
        public int SiteId { get; set; }
        public virtual Site Site { get; set; } = null!;
    }
}
