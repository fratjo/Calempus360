using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class SiteAcademicYear
    {
        public int AcademicYear_Id { get; set; }
        public int Site_Id { get; set; }
        public int University_Id { get; set; }

        public Site Site { get; set; }
    }
}
