// list classe + site
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

// TODO : Groupe Site preference // courseGroups groupé par site de préférence
// TODO : Inter site travel time (1h)
// TODO : Course not twice at the same time
// TODO : Equipement in the classroom
// TODO : Flying Equipment available
// TODO : Add Weekly hours for each course // max 2 timeplot in a row // max 4h per day // not twice on the same day not twice in a row

// site + classroom , day , hour , course, groupes

Dictionary<
    Tuple<
        ((string site, string classroom) location,
        string day,
        (int startHour, int endHour) timeSlot)>,
    (string course, List<string> groups)> GenerateSchedule()
{
    Dictionary<
        Tuple<
            ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot)>,
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
        Tuple<
            ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot)>,
        (string course, List<string> groups)> schedule,
    int currentCourseIndex)
{
    // si on a plus de cours à placer, on retourne true
    if (courseGroupes.Count == 0) return true;

    // trier les cours par capacité
    courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();

    // trier les classes par capacité
    classes = classes.OrderByDescending(c => c.Item3).ToList();

    // on prend le cours le plus gros, le premier car trié par capacité
    string currentCourse = courseGroupes[0].Course;


    // pour chaque classe (ordonné par capacité)
    for (int c = 0; c < classes.Count; c++)
    {
        var currentClass = classes[c];

        // si la capacité de la classe est suffisante
        if (currentClass.Item3 >= courseGroupes[currentCourseIndex].GetCapacity())
        {
            // on trouve un timeslot de libre pour la classe
            for (int d = 0; d < daysOfWeek.Count; d++)
            {
                var currentDay = daysOfWeek[d];

                // pour chaque timeslot de la jourée
                for (int j = 0; j < hours.Count; j++)
                {
                    var currentHour = hours[j];

                    // créer la clé de l'emplacement // ((site, classroom), day, (startHour, endHour))
                    var key = Tuple.Create(((currentClass.Item2, currentClass.Item1), currentDay, currentHour));

                    if (!schedule.ContainsKey(key)) // TODO : check if the course is not already placed at the same time
                    {
                        // on ajoute le cours au schedule
                        schedule.Add(key, (currentCourse, courseGroupes[currentCourseIndex].Groupes.Select(g => g.Name).ToList()));

                        // keep track of the current course
                        var cg = courseGroupes[currentCourseIndex];

                        // on retire du courseGroupes le cours qui vient d'être placé
                        courseGroupes.RemoveAt(currentCourseIndex);

                        // on continue avec le prochain cours
                        if (BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, currentCourseIndex)) return true;

                        // si on ne peut pas placer le prochain cours, on retire le cours actuel du schedule
                        schedule.Remove(key);

                        // on réajoute le cours retiré du courseGroupes
                        courseGroupes.Add(cg);

                        // on trie les courseGroupes restants par capacité
                        courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();
                    }
                }
            }
        }
        else // si la capacité de la classe n'est pas suffisante // on split le cours en plusieurs petits groupes
        {
            // vérifier si on peut split le cours en plusieurs petits groupes
            if (courseGroupes[currentCourseIndex].Groupes.Count == 1) return false;

            // si la capacité de la classe n'est pas suffisante, on split le cours en plusieurs petits groupes
            var groupesToPlace = BacktrackClassroomsCoursGroups(currentClass.Item3, courseGroupes[currentCourseIndex].Groupes);

            // on trouve un timeslot de libre pour la classe
            for (int d = 0; d < daysOfWeek.Count; d++)
            {
                var currentDay = daysOfWeek[d];

                // pour chaque timeslot de la jourée
                for (int j = 0; j < hours.Count; j++)
                {
                    var currentHour = hours[j];

                    // créer la clé de l'emplacement // ((site, classroom), day, (startHour, endHour))
                    var key = Tuple.Create(((currentClass.Item2, currentClass.Item1), currentDay, currentHour));

                    if (!schedule.ContainsKey(key)) // TODO : check if the course is not already placed at the same time
                    {
                        // on retire du courseGroupes le(s) groupes qui viennent d'être placés
                        courseGroupes[currentCourseIndex].RemoveGroupes(groupesToPlace);

                        // on trie les courseGroupes restants par capacité
                        courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();

                        // on place les groupes dans la classe
                        schedule.Add(key, (currentCourse, groupesToPlace.Select(g => g.Name).ToList()));

                        // on passe au prochain courseGroupe // on garde le currentCourseIndex car on prend toujours le premier cours (le plus gros)
                        if (BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, currentCourseIndex)) return true;

                        // si on ne peut pas placer le prochain coursGroupe, on le retire du schedule => free the timeslot
                        schedule.Remove(key);

                        // on réajoute les groupes retirés du courseGroupes
                        courseGroupes[currentCourseIndex].Groupes.AddRange(groupesToPlace);

                        // on trie les courseGroupes restants par capacité
                        courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();

                        // on trie les classes restantes par capacité
                        classes = classes.OrderByDescending(c => c.Item3).ToList();

                        // on passe au suivant
                        continue;
                    }
                }
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
        // Vérification de la meilleure occupation atteinte
        if (currentSum <= capacity && currentSum > bestCombinaisonCapacity)
        {
            bestCombinaisonCapacity = currentSum;
            bestCombinaison = [.. currentCombinaison];
        }

        // Parcours des groupes restants
        for (int i = index; i < groupes.Count; i++)
        {
            if (currentSum + groupes[i].Capacity <= capacity)
            {
                // Ajouter le groupe dans la combinaison actuelle
                currentCombinaison.Add(groupes[i]);

                // Appel récursif pour explorer les autres combinaisons
                Backtrack(i + 1, currentSum + groupes[i].Capacity);

                // Retirer le groupe après l'exploration (backtracking)
                currentCombinaison.RemoveAt(currentCombinaison.Count - 1);
            }
        }
    }


    Backtrack(0, 0);

    return bestCombinaison;
}

void DisplaySchedule(Dictionary<Tuple<((string site, string classroom) location, string day, (int startHour, int endHour) timeSlot)>, (string course, List<string> groups)> schedule)
{
    // sort day, time, site, classroom
    schedule = schedule.OrderBy(s => s.Key.Item1.day).ThenBy(s => s.Key.Item1.timeSlot.startHour).ThenBy(s => s.Key.Item1.location.site).ThenBy(s => s.Key.Item1.location.classroom).ToDictionary(s => s.Key, s => s.Value);

    foreach (var item in schedule)
    {
        Console.WriteLine($"Site {item.Key.Item1.location.site}, classroom {item.Key.Item1.location.classroom} : {item.Key.Item1.day} {item.Key.Item1.timeSlot.startHour}-{item.Key.Item1.timeSlot.endHour} : {item.Value.course} ({string.Join(",", item.Value.groups)})");
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