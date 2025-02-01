// list classe + site
using System.Runtime.CompilerServices;

List<(string classroom, string site, int capacity)> classes = new() {
    new("1A", "A", 40),
    new("1B", "B", 40),
    new("2A", "A", 30),
    new("2B", "B", 30),
    new("3A", "A", 50),
    new("3B", "B", 40),
    new("4A", "A", 40),
    new("4B", "B", 40)
};

// list daysOfWeek
List<string> daysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

// list hours
List<(int startHour, int endHour)> hours = new() { new(8, 10), new(10, 12), new(13, 15), new(15, 17) };

// 
List<CourseGroupes> courseGroupes = new()
{
    new CourseGroupes
    {
        Course = "Math",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
        new CourseGroupes
    {
        Course = "Math",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Physics",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "English",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "French",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "History",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Geography",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Biology",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Chemistry",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Philosophy",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Economy",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Sport",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Music",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Art",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Computer Science",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Algebra",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" },
            new Groupe { Name = "2B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes // this one is too big to fit in any classroom
    {
        Course = "Geometry",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
        }
    }
};


// --------------------------------------------
// --------------------------------------------
// --------------------------------------------

// TODO : Add Weekly hours for each course // max 2 timeplot in a row // max 4h per day // not twice on the same day not twice in a row

// site + classroom , day , hour , course, groupes

Dictionary<
    ((string site, string classroom) location,
        string day,
        (int startHour, int endHour) timeSlot),
    (string course, List<string> groups)> GenerateSchedule()
{
    Dictionary<
        ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot),
        (string course, List<string> groups)> schedule = new();

    if (classes.Count * daysOfWeek.Count * hours.Count >= courseGroupes.Sum(c => c.Groupes.Count))
    {
        if (BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, 0)) return schedule;
    }

    throw new Exception("No schedule found");
}

/// <summary>
/// Prendre le cours le plus gros, et essayer de le placer de la classe la plus grande à la plus petite
/// Si le cours est divisé, on tri le reste des cours à nouveau
/// </summary>
bool BacktrackSchedule(
    List<string> daysOfWeek,
    List<(int startHour, int endHour)> hours,
    List<CourseGroupes> courseGroupes,
    List<(string classroom, string site, int capacity)> classes,
    Dictionary<
            ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot),
        (string course, List<string> groups)> schedule,
    int index)
{
    if (courseGroupes.Count == 0 || classes.Count == 0) return true;

    courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();
    classes = classes.OrderByDescending(c => c.Item3).ToList();

    var biggestCourseGroup = courseGroupes[0];
    var biggestCourse = biggestCourseGroup.Course;
    int courseCapacity = biggestCourseGroup.GetCapacity();

    foreach (var currentClass in classes)
    {
        // TODO : Required Equipement in the classroom
        // TODO : Required Equipement is Flying Equipment & is available

        var keyAndGroups = FindTimeSlotForCourseGroup(
            currentClass.classroom,
            currentClass.site,
            currentClass.capacity,
            biggestCourse,
            biggestCourseGroup.Groupes, schedule);

        if (keyAndGroups is not null)
        {
            var (key, availableGroups) = keyAndGroups;

            if (availableGroups.Count > 0)
            {
                schedule.Add(key, (biggestCourse, availableGroups));

                biggestCourseGroup.Groupes.RemoveAll(g => availableGroups.Contains(g.Name));
                if (biggestCourseGroup.Groupes.Count == 0) courseGroupes.RemoveAt(0);

                var success = BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, 0);
                if (success) return true;

                schedule.Remove(key);
                biggestCourseGroup.Groupes.AddRange(availableGroups.Select(name => new Groupe { Name = name }));
                courseGroupes.Insert(0, biggestCourseGroup);
            }
        }
    }
    return false;
}

/// <summary>
/// Permet de retourner la liste des groupes qui remplissent de manière optimale la capacité de la classe
/// </summary>
List<Groupe> BacktrackClassroomsCoursGroups(int capacity, List<Groupe> groupes)
{
    // memoization
    List<Groupe> bestCombinaison = new();
    List<Groupe> currentCombinaison = new();
    int bestCombinaisonCapacity = 0;

    void Backtrack(int index, int currentSum)
    {
        if (currentSum <= capacity && currentSum > bestCombinaisonCapacity)
        {
            bestCombinaisonCapacity = currentSum;
            bestCombinaison = [.. currentCombinaison];
        }


        for (int i = index; i < groupes.Count; i++)
        {
            if (currentSum + groupes[i].Capacity <= capacity)
            {
                currentCombinaison.Add(groupes[i]);

                Backtrack(i + 1, currentSum + groupes[i].Capacity);

                currentCombinaison.RemoveAt(currentCombinaison.Count - 1);
            }
        }
    }

    Backtrack(0, 0);

    return bestCombinaison;
}

Tuple<((string site, string classroom) location, string day, (int startHour, int endHour) timeSlot), List<string>>? FindTimeSlotForCourseGroup(
    string classroom,
    string site,
    int capacity,
    string course,
    List<Groupe> groups,
    Dictionary<
        ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot),
        (string course, List<string> groups)> schedule
)
{
    foreach (var currentDay in daysOfWeek)
    {
        foreach (var currentHour in hours)
        {
            var key = ((site, classroom), currentDay, currentHour);

            // Check if the classroom is already in the schedule for this day and hour
            if (schedule.ContainsKey(key)) continue;

            // TODO : Inter site travel time (1h)

            // Get groups that are not already in the schedule for this day and hour
            var groupsAvailable = groups.Where(g =>
                !schedule.Any(s =>
                    s.Key.day == currentDay &&
                    s.Key.timeSlot == currentHour &&
                    s.Value.groups.Contains(g.Name)
            )).ToList();

            List<Groupe> groupsToPlace = new();

            if (groupsAvailable.Count == 1)
            {
                if (groupsAvailable[0].PreferedSite == site)
                {
                    groupsToPlace.Add(groupsAvailable[0]);
                }
            }
            else if (groupsAvailable.Count != 0)
            {
                // TODO : If all groups have the same prefered site => must be in the prefered site

                // Get groups that prefer this site
                var groupsAvailablePreferingThisSite = groupsAvailable.Where(g => g.PreferedSite == site).ToList();

                groupsToPlace = BacktrackClassroomsCoursGroups(capacity, groupsAvailablePreferingThisSite);

                if (groupsToPlace.Sum(g => g.Capacity) < capacity)
                {
                    var remainingCapacity = capacity - groupsToPlace.Sum(g => g.Capacity);
                    var groupsAvailableNotPreferingThisSite = BacktrackClassroomsCoursGroups(remainingCapacity, groupsAvailable.Except(groupsToPlace).ToList());
                    groupsToPlace.AddRange(groupsAvailableNotPreferingThisSite);
                }
            }
            else continue;

            if (groupsToPlace.Count > 0) return Tuple.Create(key, groupsToPlace.Select(g => g.Name).ToList());
        }
    }
    return null;
}


void DisplaySchedule(Dictionary<((string site, string classroom) location, string day, (int startHour, int endHour) timeSlot), (string course, List<string> groups)> schedule)
{
    // sort day, time, site, classroom
    schedule = schedule.OrderBy(s => s.Key.day).ThenBy(s => s.Key.timeSlot.startHour).ThenBy(s => s.Key.location.site).ThenBy(s => s.Key.location.classroom).ToDictionary(s => s.Key, s => s.Value);

    // Define course colors
    Dictionary<string, ConsoleColor> courseColors = new()
    {
        { "Math", ConsoleColor.Magenta },
        { "Chemistry", ConsoleColor.DarkMagenta },
        { "Physics", ConsoleColor.Cyan },
        { "Biology", ConsoleColor.DarkCyan },
        { "English", ConsoleColor.Green },
        { "Geography", ConsoleColor.DarkGreen },
        { "French", ConsoleColor.Red },
        { "History", ConsoleColor.DarkYellow },
        { "Algebra", ConsoleColor.Blue },
        { "Philosophy", ConsoleColor.DarkBlue },
        { "Economy", ConsoleColor.DarkRed },
        { "Sport", ConsoleColor.DarkGray },
        { "Music", ConsoleColor.Gray },
        { "Art", ConsoleColor.White },
        { "Computer Science", ConsoleColor.DarkGray },
        { "Geometry", ConsoleColor.DarkGreen }
    };

    foreach (var item in schedule)
    {
        // // Set site color
        // if (item.Key.Item1.location.site == "A")
        // {
        //     Console.ForegroundColor = ConsoleColor.Red;
        // }
        // else if (item.Key.Item1.location.site == "B")
        // {
        //     Console.ForegroundColor = ConsoleColor.Blue;
        // }
        // else
        // {
        //     Console.ForegroundColor = ConsoleColor.White;
        // }

        // Set course color
        if (courseColors.TryGetValue(item.Value.course, out var courseColor))
        {
            Console.ForegroundColor = courseColor;
        }

        Console.WriteLine($"Site {item.Key.location.site}, classroom {item.Key.location.classroom} : {item.Key.day} {item.Key.timeSlot.startHour}-{item.Key.timeSlot.endHour} : {item.Value.course} ({string.Join(",", item.Value.groups)})");

        // Reset color to default
        Console.ResetColor();
    }
}


var schedule = GenerateSchedule();
DisplaySchedule(schedule);

// --------------------------------------------
// --------------------------------------------
// --------------------------------------------

// groupe
class Groupe
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string PreferedSite { get; set; } = string.Empty;
}

// course + list<groupe>
class CourseGroupes
{
    public string Course { get; set; } = string.Empty;
    public List<Groupe> Groupes { get; set; } = new List<Groupe>();
    public List<string> Equipements { get; set; } = new List<string>();

    public int GetCapacity()
    {
        return Groupes.Sum(g => g.Capacity);
    }

    public void RemoveGroupes(List<Groupe> groupes)
    {
        foreach (var groupe in groupes)
        {
            Groupes.Remove(groupe);
        }
    }
}