using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Responses
{
    public class GetStudentGroupResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;
        public string OptionGrade {  get; set; } = string.Empty;
        public string Option {  get; set; } = string.Empty;
        public int NumberOfStudents { get; set; }
    }
}
