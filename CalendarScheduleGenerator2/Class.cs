using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarScheduleGenerator2
{
    public class Class
    {
        public string Classroom { get; private set; } = string.Empty;
        public string Site { get; private set; } = string.Empty;
        public int Capacity { get; private set; } = 0;
        public List<string>? Equipments { get; private set; }

        public Class(string classroom, string site, int capacity, List<string>? equipments)
        {
            Classroom = classroom;
            Site = site;
            Capacity = capacity;
            Equipments = equipments;
        }
    }
}