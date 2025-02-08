using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Models.Models
{
    public class OptionCourse
    {
        public int AcademicYear_Id { get; set; }
        public int OptionGrade {  get; set; }
        public int Course_Id { get; set; }
        public int Option_Id { get; set; }
        public Option Option {  get; set; }
        public Course Course { get; set; }
    }
}
