// list classe + site
// course + list<groupe>

namespace CalendarScheduleGenerator2
{
    public class CourseGroups
    {
        public string Course { get; set; } = string.Empty;
        public List<Groupe> Groups { get; set; } = new List<Groupe>();
        public List<Equipement>? Equipements { get; set; } = null;

        public int GetCapacity()
        {
            return Groups.Sum(g => g.Capacity);
        }

        public int GetEquipmentCount()
        {
            return Equipements?.Count ?? 0;
        }

        public void RemoveGroupes(List<Groupe> groupes)
        {
            foreach (var groupe in groupes)
            {
                Groups.Remove(groupe);
            }
        }
    }
}