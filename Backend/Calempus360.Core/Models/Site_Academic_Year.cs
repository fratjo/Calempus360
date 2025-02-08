using Calempus360_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Models.Models
{
    public class Site_Academic_Year
    {
        public int AcademicYear_Id { get; set; }
        public int Site_Id {  get; set; }
        public int University_Id { get; set; }

        public Site Site { get; set; }
    }
}
