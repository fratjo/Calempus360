List<string> courses = new() { "Math", "Physique", "Chimie", "Biologie", "EPS", "Informatique", "Anglais", "Francais", "Histoire", "Geographie", "EPS", "Musique", "Arts plastiques" };
List<string> equipementCourse = new() { "", "", "", "", "", "", "", "", "", "", "Tapis de sport", "", "" };
List<Tuple<int, int>> horaires = new() { new(8, 10), new(10, 12), new(14, 16), new(16, 18) };
List<string> classrooms = new() { "A1", "A2", "A3", "A4" };
List<string> equipementClassrooms = new() { "Tapis de sport", "", "", "" };


Dictionary<Tuple<string, int, int>, string> GenerateSchedule(
    List<string> courses,
    List<Tuple<int, int>> horaires,
    List<string> classrooms)
{
    Dictionary<Tuple<string, int, int>, string> schedule = new();

    if (BacktrackSchedule(courses, horaires, classrooms, schedule, 0))
    {
        return schedule;
    }
    throw new Exception("No schedule found");
}

bool BacktrackSchedule(
    List<string> courses,
    List<Tuple<int, int>> horaires,
    List<string> classrooms,
    Dictionary<Tuple<string, int, int>, string> schedule,
    int courseIndex)
{
    if (courseIndex == courses.Count) return true;

    string course = courses[courseIndex];

    for (int i = 0; i < horaires.Count; i++)
    {
        var timeSlot = horaires[i];

        for (int j = 0; j < classrooms.Count; j++)
        {

            Tuple<string, int, int> key = Tuple.Create(classrooms[j], timeSlot.Item1, timeSlot.Item2);
            var classroom = classrooms[j];
            if (!schedule.ContainsKey(key)) // si la salle n'est pas déjà prise à ce créneau horaire
            {
                // ----------- // ----------- // 
                // Constraints // Constraints //
                // ----------- // ----------- //

                if (equipementCourse[courseIndex] != "" && equipementClassrooms[j] != equipementCourse[courseIndex]) continue; // Equipement constraint

                if (course == "EPS" && schedule.ContainsValue("EPS"))
                {
                    var epsEntries = schedule.Where(entry => entry.Value == "EPS").ToList();
                    if (epsEntries.Any(entry => entry.Key.Item1 != classrooms[j] || entry.Key.Item2 + 2 != timeSlot.Item1))
                    {
                        continue; // EPS must be taught in the same classroom for two consecutive timeslots
                    }
                }

                if ((timeSlot.Equals(Tuple.Create(8, 10))
                    || timeSlot.Equals(Tuple.Create(10, 12))
                    || timeSlot.Equals(Tuple.Create(14, 16))
                    || classrooms[j] == "A3" || classrooms[j] == "A4")
                    && course == "Informatique") continue; // Informatique cannot be taught in A3, A4, 8-10, 10-12, 14-16

                if (course == "Anglais" && schedule.Any(entry => entry.Value == "Math" && entry.Key.Item2 == timeSlot.Item1 - 2)) continue; // Anglais cannot be taught right after Math

                // ----------- // ----------- // 
                // Constraints // Constraints //
                // ----------- // ----------- //

                schedule[key] = course;

                if (BacktrackSchedule(courses, horaires, classrooms, schedule, courseIndex + 1))
                {
                    return true;
                }

                schedule.Remove(key);
            }
        }
    }

    return false;
}

void DisplaySchedule(Dictionary<Tuple<string, int, int>, string> schedule)
{
    var sortedSchedule = schedule.OrderBy(entry => entry.Key.Item2).ThenBy(entry => entry.Value);

    foreach (var entry in sortedSchedule)
    {
        Console.WriteLine($"{entry.Key.Item2}:00-{entry.Key.Item3}:00 - {entry.Value} in {entry.Key.Item1}");
    }
}


var schedule = GenerateSchedule(courses, horaires, classrooms);
DisplaySchedule(schedule);

