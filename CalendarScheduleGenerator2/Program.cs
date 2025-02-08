using CalendarScheduleGenerator2;

List<Class> classes = new() {
    new("1A", "A", 60, null),
    new("2A", "A", 40, new List<Equipement>{ new Equipement("A", "Science Kit", Guid.NewGuid()) }),
    new("2A", "A", 60, new List<Equipement>{ new Equipement("A", "Science Kit", Guid.NewGuid()) }),
    new("2B", "B", 40, new List<Equipement>{ new Equipement("B", "Science Kit", Guid.NewGuid()) }),
    new("3B", "B", 40, new List<Equipement>{ new Equipement("B", "Microphone", Guid.NewGuid()) }),
    new("1C", "C", 60, new List<Equipement>{ new Equipement("C", "TV", Guid.NewGuid()), new Equipement("C", "Microphone", Guid.NewGuid()) }),
};

List<Equipement> flyingEquipments = new() {
    new("A", "TV", Guid.NewGuid()),
    new("A", "Microphone", Guid.NewGuid()),
    // new("B", "TV", Guid.NewGuid()),
    // new("B", "Microphone", Guid.NewGuid()),
};

List<string> daysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

List<(int startHour, int endHour)> hours = new() { new(8, 9), new(9, 10), new(10, 11), new(11, 12), new(13, 14), new(14, 15), new(15, 16), new(16, 17) };

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
            new Groupe { Name = "2B", Capacity = 30, PreferedSite = "B" },
            new Groupe { Name = "3B", Capacity = 35, PreferedSite = "B" }
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
            new Groupe { Name = "2B", Capacity = 30, PreferedSite = "B" },
            new Groupe { Name = "3B", Capacity = 35, PreferedSite = "B" }
        }
    },
    new CourseGroupes
    {
        Course = "Physics",
        Equipements = new List<Equipement>{ new Equipement(null, "Science Kit", null) },
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
        Equipements = new List<Equipement>{ new Equipement(null, "TV", null) },
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
        Equipements = new List<Equipement>{ new Equipement(null, "TV", null) },
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
        Equipements = new List<Equipement>{
            new Equipement(null, "TV", null),
            new Equipement(null, "Microphone", null) },
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
        Equipements = new List<Equipement>{ new Equipement(null, "Science Kit", null) },
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
        Equipements = new List<Equipement>{ new Equipement(null, "Science Kit", null) },
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
    new CourseGroupes
    {
        Course = "Geometry",
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
        Course = "Trigonometry",
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
        Course = "Calculus",
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
        Course = "Statistics",
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
        Course = "Probability",
        Groupes = new List<Groupe>
        {
            new Groupe { Name = "1A", Capacity = 25, PreferedSite = "A" },
            new Groupe { Name = "1B", Capacity = 20, PreferedSite = "B" },
            new Groupe { Name = "2A", Capacity = 30, PreferedSite = "A" }
        }
    }
};

// TODO : Add Weekly hours for each course // max 2 timeplot in a row // max 4h per day // not twice on the same day not twice in a row

// TODO : Add more constraints before running the schedule

var ScheduleGenerator = new ScheduleGenerator(
    classes,
    daysOfWeek,
    hours,
    courseGroupes,
    flyingEquipments
);

ScheduleGenerator.GenerateSchedule();
ScheduleGenerator.DisplaySchedule();