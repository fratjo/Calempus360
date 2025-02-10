using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class CourseEquipmentType
    {
        public int AcademicYear_Id { get; set; }
        public int Course_Id { get; set; }
        public int EquipmentType_Id { get; set; }
        public int Quantity { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public Course Course { get; set; }
    }
}
