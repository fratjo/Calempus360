using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Errors.StudentGroup
{
    public class InvalidStudentGroupException : Exception
    {
        public InvalidStudentGroupException() : base("The student Group properties are invalid !")
        {

        }
    }
}
