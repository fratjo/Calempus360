namespace ScheduleGenerator
{
    public class Schedule : Dictionary<ScheduleKey, ScheduleEntry>
    {
        public void AddEntry(string site, string classroom, string day, int startHour, int endHour,
                        string course, List<string> groups, List<Equipement> flyingEquipments)
        {
            var key = new ScheduleKey((site, classroom), day, (startHour, endHour));
            var entry = new ScheduleEntry(course, groups, flyingEquipments);
            this[key] = entry;
        }

        public bool IsScheduled(string site, string classroom, string day, int startHour, int endHour)
        {
            var key = new ScheduleKey((site, classroom), day, (startHour, endHour));
            return this.ContainsKey(key);
        }
    }
    public class ScheduleKey
    {
        public (string Site, string Classroom) Location { get; set; }
        public string Day { get; set; }
        public (int StartHour, int EndHour) TimeSlot { get; set; }

        public ScheduleKey((string, string) location, string day, (int, int) timeSlot)
        {
            Location = location;
            Day = day;
            TimeSlot = timeSlot;
        }
        public override bool Equals(object? obj)
        {
            return obj is ScheduleKey key &&
                Location.Equals(key.Location) &&
                Day == key.Day &&
                TimeSlot.Equals(key.TimeSlot);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Location, Day, TimeSlot);
        }
    }

    public class ScheduleEntry
    {
        public string Course { get; set; }
        public List<string> Groups { get; set; }
        public List<Equipement> FlyingEquipments { get; set; }
        public ScheduleEntry(string course, List<string> groups, List<Equipement> flyingEquipments)
        {
            Course = course;
            Groups = groups;
            FlyingEquipments = flyingEquipments;
        }
    }
}