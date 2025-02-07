using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Models.Models
{
    public class DayWithoutCourse
    {
        public int DayWithoutCourse_Id{ get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AcademicYear_Id { get; set; }
        public Academic_Year AcademicYear { get; set; }
    }
}
