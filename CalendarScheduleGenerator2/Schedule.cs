using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarScheduleGenerator2
{
    public class Schedule : Dictionary<Key, Entry>
    {
        public void AddEntry(string site, string classroom, string day, int startHour, int endHour,
                        string course, List<string> groups, List<string> flyingEquipments)
        {
            var key = new Key((site, classroom), day, (startHour, endHour));
            var entry = new Entry(course, groups, flyingEquipments);
            this[key] = entry;
        }

        public bool IsScheduled(string site, string classroom, string day, int startHour, int endHour)
        {
            var key = new Key((site, classroom), day, (startHour, endHour));
            return this.ContainsKey(key);
        }
    }
    public class Key
    {
        public (string Site, string Classroom) Location { get; set; }
        public string Day { get; set; }
        public (int StartHour, int EndHour) TimeSlot { get; set; }

        public Key((string, string) location, string day, (int, int) timeSlot)
        {
            Location = location;
            Day = day;
            TimeSlot = timeSlot;
        }
        public override bool Equals(object obj)
        {
            return obj is Key key &&
                Location.Equals(key.Location) &&
                Day == key.Day &&
                TimeSlot.Equals(key.TimeSlot);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Location, Day, TimeSlot);
        }
    }

    public class Entry
    {
        public string Course { get; set; }
        public List<string> Groups { get; set; }
        public List<string> FlyingEquipments { get; set; }
        public Entry(string course, List<string> groups, List<string> flyingEquipments)
        {
            Course = course;
            Groups = groups;
            FlyingEquipments = flyingEquipments;
        }
    }
}