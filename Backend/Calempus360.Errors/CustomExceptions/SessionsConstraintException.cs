using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calempus360.Errors.CustomExceptions
{
    public class SessionConstraintException(string message) : Exception(message);
    public class ItemNotAvailableException(string message) : Exception(message);
}