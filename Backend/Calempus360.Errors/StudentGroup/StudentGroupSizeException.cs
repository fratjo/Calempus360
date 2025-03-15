using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Errors.StudentGroup
{
    public class StudentGroupSizeException : Exception
    {
        public StudentGroupSizeException() : base("The size of a group must be beetwen 20 and 40 students !") { }
    }
}
