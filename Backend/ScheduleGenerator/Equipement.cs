namespace ScheduleGenerator
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