using ScheduleGenerator;

namespace ScheduleGeneratorRunner;

public class Program
{
    static void Main(string[] args)
    {
        List<Class> classes = new()
        {
            new("1A", "A", 60, null),

            new("2A", "A", 60,
                new List<Equipement>
                    { new Equipement("A", "Science Kit", Guid.NewGuid()) }),

            new("2B", "B", 40,
                new List<Equipement>
                    { new Equipement("B", "Science Kit", Guid.NewGuid()) }),

            new("3B", "B", 40,
                new List<Equipement> { new Equipement("B", "Microphone", Guid.NewGuid()) }),

            new("1C", "C", 60,
                new List<Equipement>
                {
                    //new Equipement("C", "TV",         Guid.NewGuid()),
                    //new Equipement("C", "Microphone", Guid.NewGuid())
                }),
        };

        List<Equipement> flyingEquipments = new()
        {
            new Equipement("A", "TV",         Guid.NewGuid()),
            new Equipement("A", "Microphone", Guid.NewGuid())
            // new("B", "TV", Guid.NewGuid()),
            // new("B", "Microphone", Guid.NewGuid()),
        };

        List<string> daysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        List<(int startHour, int endHour)> hours = new()
        {
            new(8, 9), new(9, 10), new(10, 11), new(11, 12),
            new(13, 14), new(14, 15), new(15, 16), new(16, 17)
        };

        List<(string site, string dayOfWeek, (int startHour, int endHour))> openingHours = new(){
            ("A", "Monday", (8, 12)), ("A", "Monday", (13, 17)),
            ("A", "Tuesday", (8, 12)), ("A", "Tuesday", (13, 17)),
            ("A", "Wednesday", (8, 12)), ("A", "Wednesday", (13, 17)),
            ("A", "Thursday", (8, 12)), ("A", "Thursday", (13, 17)),
            ("A", "Friday", (8, 12)), ("A", "Friday", (13, 17)),
            ("B", "Monday", (8, 12)), ("B", "Monday", (13, 17)),
            ("B", "Tuesday", (8, 12)), ("B", "Tuesday", (13, 17)),
            ("B", "Wednesday", (8, 12)), ("B", "Wednesday", (13, 17)),
            ("B", "Thursday", (8, 12)), ("B", "Thursday", (13, 17)),
            ("B", "Friday", (8, 12)), ("B", "Friday", (13, 17)),
            ("C", "Monday", (8, 12)), ("C", "Monday", (13, 17)),
            ("C", "Tuesday", (8, 12)), ("C", "Tuesday", (13, 17)),
            ("C", "Wednesday", (8, 12)), ("C", "Wednesday", (13, 17)),
            ("C", "Thursday", (8, 12)), ("C", "Thursday", (13, 17)),
            ("C", "Friday", (8, 12)), ("C", "Friday", (13, 17))
        };

        List<CourseGroups> courseGroups = new()
        {
            new CourseGroups
            {
                Course = "Math",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A",
                        Capacity     = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B",
                        Capacity     = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A",
                        Capacity     = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B",
                        Capacity     = 30,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "3B",
                        Capacity     = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Math",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 30,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "3B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Physics",
                Equipements = new List<Equipement>
                    { new Equipement(null, "Science Kit", null) },
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "English",
                Equipements = new List<Equipement>
                    { new Equipement(null, "TV", null) },
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "French",
                Equipements = new List<Equipement>
                    { new Equipement(null, "TV", null) },
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "History",
                Equipements = new List<Equipement>
                {
                    new Equipement(null, "TV",         null),
                    new Equipement(null, "Microphone", null)
                },
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Geography",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Biology",
                Equipements = new List<Equipement>
                    { new Equipement(null, "Science Kit", null) },
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Chemistry",
                Equipements = new List<Equipement>
                    { new Equipement(null, "Science Kit", null) },
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Philosophy",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Economy",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Sport",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Music",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Art",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Computer Science",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Algebra",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Geometry",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Trigonometry",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Calculus",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Statistics",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "2B", Capacity = 35,
                        PreferedSite = "B"
                    }
                }
            },
            new CourseGroups
            {
                Course = "Probability",
                Groups = new List<Group>
                {
                    new Group
                    {
                        Name         = "1A", Capacity = 25,
                        PreferedSite = "A"
                    },
                    new Group
                    {
                        Name         = "1B", Capacity = 20,
                        PreferedSite = "B"
                    },
                    new Group
                    {
                        Name         = "2A", Capacity = 30,
                        PreferedSite = "A"
                    }
                }
            }
        };

        // TODO : Add Weekly hours for each course // max 2 timeplot in a row // max 4h per day // not twice on the same day not twice in a row

        // TODO : Add more constraints before running the schedule

        var scheduleGenerator = new ScheduleGenerator.ScheduleGenerator(
            classes,
            daysOfWeek,
            hours,
            courseGroups,
            flyingEquipments
        );

        scheduleGenerator.GenerateSchedule();
        scheduleGenerator.DisplaySchedule();
    }
}