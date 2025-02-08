using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Models.Models
{
    public class UniversitySiteEquipment
    {
        public int AcademicYear_Id { get; set; }
        public int Equipment_Id { get; set; }
        public int Site_Id { get; set; }
        public int University_Id { get; set; }
        public University University { get; set; }
        public Site Site { get; set; }
        public Equipment Equipment { get; set; }   
    }
}
