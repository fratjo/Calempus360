// list classe + site
// course + list<groupe>

namespace CalendarScheduleGenerator2
{
    public class CourseGroups
    {
        public string Course { get; set; } = string.Empty;
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Equipement>? Equipements { get; set; } = null;

        public int GetCapacity()
        {
            return Groups.Sum(g => g.Capacity);
        }

        public int GetEquipmentCount()
        {
            return Equipements?.Count ?? 0;
        }

        public void RemoveGroups(List<Group> groupes)
        {
            foreach (var groupe in groupes)
            {
                Groups.Remove(groupe);
            }
        }
    }
}