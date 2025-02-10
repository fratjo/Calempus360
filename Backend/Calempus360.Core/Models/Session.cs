using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.Models
{
    public class Session
    {
        public int Session_Id { get; set; }
        public DateTime DatetimeStart { get; set; }
        public DateTime DatetimeEnd { get; set; }
        public int Classroom_Id { get; set; }
        public int Course_Id { get; set; }
        public Classroom Classroom { get; set; }
        public Course Course { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<Group> Groups { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
