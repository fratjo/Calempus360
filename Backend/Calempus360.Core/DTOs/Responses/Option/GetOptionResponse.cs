using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Responses.Option
{
    public class GetOptionResponse
    {
        public string Name {  get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Courses { get; set; } = new List<string>();
    }
}
