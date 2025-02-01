// list classe + site
using System.Runtime.CompilerServices;

List<(string classroom, string site, int capacity, List<string>? equipments)> classes = new() {
    new("1A", "A", 40, null),
    new("2A", "A", 40, new(){"Science Kit"}),
    new("3A", "A", 40, null),
    new("1B", "B", 60, new(){"TV"}),
    new("2B", "B", 40, new(){"Science Kit"}),
    new("3B", "B", 90, new(){"Microphone"}),
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
        Equipements = new(){"TV"},
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

    if ((classes.Count * daysOfWeek.Count * hours.Count - classes.Count * daysOfWeek.Count * hours.Count * 0.1) >= courseGroupes.Sum(c => c.Groupes.Count)) // If nb slot is enough to place all groups
    {
        if (BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, 0)) return schedule;
    }
    else if (courseGroupes.Sum(c => c.GetCapacity()) <= (classes.Sum(c => c.capacity) * daysOfWeek.Count * hours.Count - classes.Count * daysOfWeek.Count * hours.Count * 0.1)) // If capacity is enough to place all groups
    {
        if (BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, 0)) return schedule;
    }
    else throw new Exception("No schedule possible");

    System.Console.WriteLine("No schedule found");

    return schedule;
}


/// <summary>
/// Prendre le cours le plus gros, et essayer de le placer de la classe la plus grande à la plus petite
/// Si le cours est divisé, on tri le reste des cours à nouveau
/// </summary>
bool BacktrackSchedule(
    List<string> daysOfWeek,
    List<(int startHour, int endHour)> hours,
    List<CourseGroupes> courseGroupes,
    List<(string classroom, string site, int capacity, List<string>? equipments)> classes,
    Dictionary<
            ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot),
        (string course, List<string> groups)> schedule,
    int index)
{
    if (courseGroupes.Count == 0 || classes.Count == 0) return true;

    courseGroupes = courseGroupes.OrderByDescending(c => c.GetEquipmentCount()).ThenByDescending(c => c.GetCapacity()).ToList();
    classes = classes.OrderByDescending(c => c.Item3).ToList();

    var biggestCourseGroup = courseGroupes[0];
    var biggestCourse = biggestCourseGroup.Course;
    int courseCapacity = biggestCourseGroup.GetCapacity();

    foreach (var currentClass in classes)
    {
        if (biggestCourseGroup.Equipements is not null && biggestCourseGroup.Equipements.Count > 0)
        {
            if (currentClass.equipments is null) continue;
            if (!biggestCourseGroup.Equipements.All(e => currentClass.equipments.Contains(e))) continue;
        }


        // TODO : Required Equipement is Flying Equipment & is available

        var keyAndGroups = FindTimeSlotForCourseGroup(
            currentClass,
            biggestCourseGroup, schedule);

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
    (string classroom, string site, int capacity, List<string>? equipments) currentClass,
    CourseGroupes courseGroup,
    Dictionary<
        ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot),
        (string course, List<string> groups)> schedule
)
{
    var groups = courseGroup.Groupes;
    var course = courseGroup.Course;
    var requiredEquipment = courseGroup.Equipements;

    foreach (var currentDay in daysOfWeek)
    {
        foreach (var currentHour in hours)
        {
            if (schedule.Any(s => s.Key.day == currentDay && s.Key.timeSlot == currentHour && s.Value.course == course)) continue;

            var key = ((currentClass.site, currentClass.classroom), currentDay, currentHour);

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
                var isPeferedSiteAvailable = !IsSiteFullForTimeSlot(groupsAvailable[0].PreferedSite, classes, schedule);
                var isPreferedSite = groupsAvailable[0].PreferedSite == currentClass.site;

                // est-ce que au moins une de mes classes du site préféré possède les équipements requis
                var havePreferedSiteRequiredEquipments = classes.Any(c =>
                    c.site == groupsAvailable[0].PreferedSite &&
                    requiredEquipment is not null &&
                    c.equipments is not null &&
                    requiredEquipment.All(e => c.equipments.Contains(e)));

                if ((isPeferedSiteAvailable && isPreferedSite) ||
                    (!isPeferedSiteAvailable && !isPreferedSite) ||
                    (isPeferedSiteAvailable && !isPreferedSite && !havePreferedSiteRequiredEquipments))
                {
                    groupsToPlace = groupsAvailable;
                }
            }
            else if (groupsAvailable.Count != 0)
            {
                var groupsAvailablePreferingThisSite = groupsAvailable.Where(g => g.PreferedSite == currentClass.site).ToList();

                groupsToPlace = BacktrackClassroomsCoursGroups(currentClass.capacity, groupsAvailablePreferingThisSite);

                if (groupsToPlace.Sum(g => g.Capacity) < currentClass.capacity)
                {
                    var remainingCapacity = currentClass.capacity - groupsToPlace.Sum(g => g.Capacity);
                    var remainingGroups = groupsAvailable.Except(groupsToPlace).ToList();
                    var groupsAvailableNotPreferingThisSite = BacktrackClassroomsCoursGroups(remainingCapacity, remainingGroups);
                    groupsToPlace.AddRange(groupsAvailableNotPreferingThisSite);
                }
            }
            else continue;

            if (groupsToPlace.Count > 0) return Tuple.Create(key, groupsToPlace.Select(g => g.Name).ToList());
        }
    }
    return null;
}

bool IsSiteFullForTimeSlot(
    string site,
    List<(string classroom, string site, int capacity, List<string>? equipments)> classes,
    Dictionary<
        ((string site, string classroom) location,
        string day,
        (int startHour, int endHour) timeSlot),
        (string course, List<string> groups)> schedule
)
{
    int maxTimeSlotsForSite = classes
        .Count(c => c.site == site) * daysOfWeek.Count * hours.Count;

    int usedTimeSlotsForSite = schedule
        .Count(entry => entry.Key.location.site == site);

    return usedTimeSlotsForSite >= maxTimeSlotsForSite;
}

void DisplaySchedule(Dictionary<((string site, string classroom) location, string day, (int startHour, int endHour) timeSlot), (string course, List<string> groups)> schedule)
{
    // sort day, time, site, classroom
    schedule = schedule.OrderBy(s => s.Key.day).ThenBy(s => s.Key.timeSlot.startHour).ThenBy(s => s.Key.location.site).ThenBy(s => s.Key.location.classroom).ToDictionary(s => s.Key, s => s.Value);

    foreach (var item in schedule)
    {
        // Set site color
        if (item.Key.location.site == "A")
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (item.Key.location.site == "B")
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
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
    public List<string>? Equipements { get; set; } = new List<string>();

    public int GetCapacity()
    {
        return Groupes.Sum(g => g.Capacity);
    }

    public int GetEquipmentCount()
    {
        return Equipements?.Count ?? 0;
    }

    public void RemoveGroupes(List<Groupe> groupes)
    {
        foreach (var groupe in groupes)
        {
            Groupes.Remove(groupe);
        }
    }
}