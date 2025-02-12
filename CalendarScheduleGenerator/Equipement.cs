using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarScheduleGenerator2
{
    public class Equipement
    {
        public string? Site { get; private set; } = string.Empty;
        public string Type { get; private set; }
        public Guid? Code { get; private set; } = Guid.Empty;
        public Equipement(string? site, string type, Guid? code)
        {
            Site = site;
            Type = type;
            Code = code;
        }
    }
}