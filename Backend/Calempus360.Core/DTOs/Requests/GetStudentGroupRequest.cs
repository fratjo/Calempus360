using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Requests
{
    public class GetStudentGroupRequest
    {
        public string Code { get; set; } = string.Empty;
        public int OptionGrade { get; set; }
        public string Option { get; set; } = string.Empty;
        public int NumberOfStudents { get; set; }
        public string Site { get; set; } = string.Empty;
    }
}
