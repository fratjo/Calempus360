using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Models.Models
{
    public class ClassroomAcademicYear
    {
        public int AcademicYear_Id { get; set; }
        public int Classroom_Id { get; set; }
        public int Site_Id { get; set; }
        public Classroom Classroom { get; set; }

    }
}
