using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Models.Models
{
    public class EquipmentType
    {
        public int EquipmentType_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<CourseEquipmentType> Courses { get; set; }
    }
}
