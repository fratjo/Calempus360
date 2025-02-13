using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class EquipmentType
    {
        public Guid EquipmentTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // Equipment
        public virtual List<Equipment> Equipments { get; set; }
        
        // CourseEquipmentType
        public virtual List<CourseEquipmentType> CourseEquipmentTypes { get; set; }
    }
}
