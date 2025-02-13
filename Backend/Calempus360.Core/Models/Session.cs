using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public DateTime DatetimeStart { get; set; }
        public DateTime DatetimeEnd { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation Properties
        
        // Classroom
        public Guid ClassroomId { get; set; }
        public virtual Classroom Classroom { get; set; } = null!;
        
        // Course
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
        
        //  EquipmentSessions
        public virtual List<EquipmentSession> EquipmentSessions { get; set; }
        
        // StudentGroupSessions
        public virtual List<StudentGroupSession> StudentGroupSessions { get; set; }
    }
}
