namespace ScheduleGenerator
{
    public class Class
    {
        public string Classroom { get; private set; } = string.Empty;
        public string Site { get; private set; } = string.Empty;
        public int Capacity { get; private set; } = 0;
        public List<Equipement>? Equipments { get; private set; }

        public Class(string classroom, string site, int capacity, List<Equipement>? equipments)
        {
            Classroom = classroom;
            Site = site;
            Capacity = capacity;
            Equipments = equipments;
        }
    }
}