namespace ScheduleGenerator
{
    public class Class
    {
        public string Name { get; private set; } = string.Empty;
        public string Site { get; private set; } = string.Empty;
        public int Capacity { get; private set; } = 0;
        public List<Equipement>? Equipments { get; private set; }

        public Class(string name, string site, int capacity, List<Equipement>? equipments)
        {
            Name = name;
            Site = site;
            Capacity = capacity;
            Equipments = equipments;
        }
    }
}