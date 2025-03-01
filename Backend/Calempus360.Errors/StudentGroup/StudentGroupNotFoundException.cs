using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Errors.StudentGroup
{
    public class StudentGroupNotFoundException(Guid id) : Exception($"Student Group with Id : {id} not found !")
    {

    }
}
