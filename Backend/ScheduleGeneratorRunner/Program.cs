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

        // List<string> daysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        // List<(int startHour, int endHour)> hours = new()
        // {
        //     new(8, 9), new(9, 10), new(10, 11), new(11, 12),
        //     new(13, 14), new(14, 15), new(15, 16), new(16, 17)
        // };

        List<(string site, string dayOfWeek, (int startHour, int endHour))> openingHours = new(){
            /*("A", "Monday", (8, 9)),*/ ("A", "Monday", (9, 10)), ("A", "Monday", (10, 11)), ("A", "Monday", (11, 12)),
            ("A", "Monday", (13, 14)), ("A", "Monday", (14, 15)), ("A", "Monday", (15, 16)), ("A", "Monday", (16, 17)),
            ("A", "Tuesday", (8, 9)), ("A", "Tuesday", (9, 10)), ("A", "Tuesday", (10, 11)), ("A", "Tuesday", (11, 12)),
            ("A", "Tuesday", (13, 14)), ("A", "Tuesday", (14, 15)), ("A", "Tuesday", (15, 16)), ("A", "Tuesday", (16, 17)),
            ("A", "Wednesday", (8, 9)), ("A", "Wednesday", (9, 10)), ("A", "Wednesday", (10, 11)), ("A", "Wednesday", (11, 12)),
            ("A", "Wednesday", (13, 14)), ("A", "Wednesday", (14, 15)), ("A", "Wednesday", (15, 16)), ("A", "Wednesday", (16, 17)),
            ("A", "Thursday", (8, 9)), ("A", "Thursday", (9, 10)), ("A", "Thursday", (10, 11)), ("A", "Thursday", (11, 12)),
            ("A", "Thursday", (13, 14)), ("A", "Thursday", (14, 15)), ("A", "Thursday", (15, 16)), ("A", "Thursday", (16, 17)),
            ("A", "Friday", (8, 9)), ("A", "Friday", (9, 10)), ("A", "Friday", (10, 11)), ("A", "Friday", (11, 12)),
            ("A", "Friday", (13, 14)), ("A", "Friday", (14, 15)), ("A", "Friday", (15, 16)), ("A", "Friday", (16, 17)),
            ("B", "Monday", (8, 9)), ("B", "Monday", (9, 10)), ("B", "Monday", (10, 11)), ("B", "Monday", (11, 12)),
            ("B", "Monday", (13, 14)), ("B", "Monday", (14, 15)), ("B", "Monday", (15, 16)), ("B", "Monday", (16, 17)),
            ("B", "Tuesday", (8, 9)), ("B", "Tuesday", (9, 10)), ("B", "Tuesday", (10, 11)), ("B", "Tuesday", (11, 12)),
            ("B", "Tuesday", (13, 14)), ("B", "Tuesday", (14, 15)), ("B", "Tuesday", (15, 16)), ("B", "Tuesday", (16, 17)),
            ("B", "Wednesday", (8, 9)), ("B", "Wednesday", (9, 10)), ("B", "Wednesday", (10, 11)), ("B", "Wednesday", (11, 12)),
            ("B", "Wednesday", (13, 14)), ("B", "Wednesday", (14, 15)), ("B", "Wednesday", (15, 16)), ("B", "Wednesday", (16, 17)),
            ("B", "Thursday", (8, 9)), ("B", "Thursday", (9, 10)), ("B", "Thursday", (10, 11)), ("B", "Thursday", (11, 12)),
            ("B", "Thursday", (13, 14)), ("B", "Thursday", (14, 15)), ("B", "Thursday", (15, 16)), ("B", "Thursday", (16, 17)),
            ("B", "Friday", (8, 9)), ("B", "Friday", (9, 10)), ("B", "Friday", (10, 11)), ("B", "Friday", (11, 12)),
            ("B", "Friday", (13, 14)), ("B", "Friday", (14, 15)), ("B", "Friday", (15, 16)), ("B", "Friday", (16, 17)),
            ("C", "Monday", (8, 9)), ("C", "Monday", (9, 10)), ("C", "Monday", (10, 11)), ("C", "Monday", (11, 12)),
            ("C", "Monday", (13, 14)), ("C", "Monday", (14, 15)), ("C", "Monday", (15, 16)), ("C", "Monday", (16, 17)),
            ("C", "Tuesday", (8, 9)), ("C", "Tuesday", (9, 10)), ("C", "Tuesday", (10, 11)), ("C", "Tuesday", (11, 12)),
            ("C", "Tuesday", (13, 14)), ("C", "Tuesday", (14, 15)), ("C", "Tuesday", (15, 16)), ("C", "Tuesday", (16, 17)),
            ("C", "Wednesday", (8, 9)), ("C", "Wednesday", (9, 10)), ("C", "Wednesday", (10, 11)), ("C", "Wednesday", (11, 12)),
            ("C", "Wednesday", (13, 14)), ("C", "Wednesday", (14, 15)), ("C", "Wednesday", (15, 16)), ("C", "Wednesday", (16, 17)),
            ("C", "Thursday", (8, 9)), ("C", "Thursday", (9, 10)), ("C", "Thursday", (10, 11)), ("C", "Thursday", (11, 12)),
            ("C", "Thursday", (13, 14)), ("C", "Thursday", (14, 15)), ("C", "Thursday", (15, 16)), ("C", "Thursday", (16, 17)),
            ("C", "Friday", (8, 9)), ("C", "Friday", (9, 10)), ("C", "Friday", (10, 11)), ("C", "Friday", (11, 12)),
            ("C", "Friday", (13, 14)), ("C", "Friday", (14, 15)), ("C", "Friday", (15, 16)), ("C", "Friday", (16, 17))
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
            // daysOfWeek,
            // hours,
            openingHours,
            courseGroups,
            flyingEquipments
        );

        scheduleGenerator.GenerateSchedule();
        scheduleGenerator.DisplaySchedule();
    }
}