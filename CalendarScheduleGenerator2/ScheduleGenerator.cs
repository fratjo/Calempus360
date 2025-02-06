using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarScheduleGenerator2
{
    // TODO : Use Equipement Code instead of Name for flyingEquipments and Equipements
    public class ScheduleGenerator
    {
        private readonly List<Class> classes;
        private readonly List<string> daysOfWeek;
        private readonly List<(int startHour, int endHour)> hours;
        private readonly List<CourseGroupes> courseGroupes;
        private readonly List<Equipement> flyingEquipments;
        public Schedule schedule { get; private set; } = new Schedule();

        public ScheduleGenerator(
                List<Class> classes,
                List<string> daysOfWeek,
                List<(int startHour, int endHour)> hours,
                List<CourseGroupes> courseGroupes,
                List<Equipement> flyingEquipments)
        {
            this.classes = classes;
            this.daysOfWeek = daysOfWeek;
            this.hours = hours;
            this.courseGroupes = courseGroupes;
            this.flyingEquipments = flyingEquipments;
        }

        public Schedule GenerateSchedule()
        {
            System.Console.WriteLine("Generating schedule...");
            var timeSlots = classes.Count * daysOfWeek.Count * hours.Count;
            System.Console.WriteLine("Time slots (reserve: 5%) : " + (timeSlots - timeSlots * 0.05));
            var groupsCount = courseGroupes.Sum(c => c.Groupes.Count);
            System.Console.WriteLine("Groups count : " + groupsCount);
            var classesCapacity = classes.Sum(c => c.Capacity) * daysOfWeek.Count * hours.Count;
            System.Console.WriteLine("Capacity (reserve: 5%): " + (classesCapacity - (classesCapacity * 0.05)));
            var groupsCapacity = courseGroupes.Sum(c => c.GetCapacity());
            System.Console.WriteLine("Groups capacity: " + groupsCapacity);

            if ((timeSlots - timeSlots * 0.05) < groupsCount) // If nb slot is enough to place all groups
            {
                throw new Exception("No schedule possible: not enough slots.");
            }
            else if (classesCapacity - (classesCapacity * 0.05) < groupsCapacity) // If capacity is enough to place all groups
            {
                throw new Exception("No schedule possible: not enough capacity.");
            }
            else if (!BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule)) // If no schedule found
            {
                throw new Exception("No schedule found");
            }

            System.Console.WriteLine("Schedule generated");

            return schedule;
        }

        private bool BacktrackSchedule(
            List<string> daysOfWeek,
            List<(int startHour, int endHour)> hours,
            List<CourseGroupes> courseGroupes,
            List<Class> classes,
            Schedule schedule)
        {
            if (courseGroupes.Count == 0 || classes.Count == 0) return true;

            courseGroupes = courseGroupes.OrderByDescending(c => c.GetEquipmentCount()).ThenByDescending(c => c.GetCapacity()).ToList();
            classes = classes.OrderByDescending(c => c.Capacity).ToList();


            var biggestCourseGroup = courseGroupes[0];
            var biggestCourse = biggestCourseGroup.Course;
            int courseCapacity = biggestCourseGroup.GetCapacity();

            foreach (var currentClass in classes)
            {
                if (biggestCourseGroup.Equipements is not null && biggestCourseGroup.Equipements.Any()) // si j'ai besoin d'équipements
                {
                    var allInFlyingEquipments = biggestCourseGroup.Equipements.All(e =>
                                                        flyingEquipments.Any(f => f.Site == currentClass.Site && f.Type == e.Type));
                    if (currentClass.Equipments is not null && currentClass.Equipments.Any()) // si la classe possède des équipements
                    {
                        var allInClassroom = biggestCourseGroup.Equipements.All(e => currentClass.Equipments.Any(ce => ce.Type == e.Type));
                        var allInBothClassroomAndSite = biggestCourseGroup.Equipements.All(e => currentClass.Equipments.Any(ce => ce.Type == e.Type) ||
                                                            (!currentClass.Equipments.Any(ce => ce.Type == e.Type) &&
                                                            flyingEquipments.Where(f => f.Site == currentClass.Site).Any(f => f.Type == e.Type)));

                        if (!allInClassroom && !allInBothClassroomAndSite)
                            continue;
                    } // si la classe ne possède pas les équipements requis et que les équipements requis ne sont pas tous disponibles sur le site
                    else if (!allInFlyingEquipments) continue;

                }
                // Je sais qu'ici la classe possède les équipements requis ou que les équipements requis, sauf ceux qui sont déjà dans la classe, sont disponibles sur le site

                var keyAndGroups = FindTimeSlotForCourseGroup(
                    currentClass,
                    biggestCourseGroup, schedule);

                if (keyAndGroups is not null)
                {
                    var (key, entry) = keyAndGroups;

                    if (entry.Groups.Count > 0)
                    {
                        schedule.Add(key, entry);

                        biggestCourseGroup.Groupes.RemoveAll(g => entry.Groups.Contains(g.Name));
                        if (biggestCourseGroup.Groupes.Count == 0) courseGroupes.RemoveAt(0);

                        var success = BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule);
                        if (success) return true;

                        schedule.Remove(key);
                        biggestCourseGroup.Groupes.AddRange(entry.Groups.Select(name => new Groupe { Name = name }));
                        courseGroupes.Insert(0, biggestCourseGroup);
                    }
                }
            }
            return false;
        }

        Tuple<ScheduleKey, ScheduleEntry>? FindTimeSlotForCourseGroup(
            Class currentClass,
            CourseGroupes courseGroup,
            Schedule schedule
        )
        {
            var groups = courseGroup.Groupes;
            var course = courseGroup.Course;
            var requiredEquipment = courseGroup.Equipements;

            foreach (var currentDay in daysOfWeek) // ("lundi", 8, 10)
            {
                foreach (var currentHour in hours)
                {
                    // if (schedule.Any(s =>
                    //     s.Key.Day == currentDay &&
                    //     s.Key.TimeSlot == currentHour &&
                    //     s.Value.Course == course &&
                    //     s.Key.Location.Site == currentClass.Site)) continue;

                    var SK = new ScheduleKey((currentClass.Site, currentClass.Classroom), currentDay, currentHour);

                    // Check if the classroom is already in the schedule for this day and hour
                    if (schedule.ContainsKey(SK)) continue;

                    // Equipements volants pour le site de la classe actuelle
                    var currentSiteFlyingEquipments = flyingEquipments.Where(f => f.Site == currentClass.Site).Select(f => f).ToList();

                    // Tous les équipements pris pour ce jour et cette heure, pour le site de la classe actuelle
                    List<Equipement> takenEq = schedule.Where(s =>
                                                        s.Key.Day == currentDay &&
                                                        s.Key.TimeSlot == currentHour &&
                                                        s.Key.Location.Site == currentClass.Site).SelectMany(s => s.Value.FlyingEquipments).ToList();

                    // Equipements volants disponibles pour ce jour et cette heure
                    var availableEq = currentSiteFlyingEquipments.Where(e => !takenEq.Any(eq => eq.Code == e.Code)).ToList();

                    var currentRequiredFlyingEquipments = requiredEquipment;

                    if (requiredEquipment is not null && requiredEquipment.Any())
                    {
                        // Check if the equipements are available for this day and hour
                        if (currentClass.Equipments is not null && currentClass.Equipments.Any())
                        {
                            currentRequiredFlyingEquipments = requiredEquipment.Where(e => !currentClass.Equipments.Any(ce => e.Type == ce.Type)).ToList(); // Equipements requis que la classe ne possède pas

                            if (currentRequiredFlyingEquipments.Any())
                            {
                                // je dois vérifier si dans les équipements volants disponibles, j'ai tous les équipements requis que la classe ne possède pas
                                if (!currentRequiredFlyingEquipments.All(e => availableEq.Any(eq => eq.Type == e.Type))) continue;
                            }
                        }
                        else
                        {
                            // je dois vérifier si dans les équipements volants disponibles, j'ai tous les équipements requis que la classe ne possède pas
                            if (!requiredEquipment.All(e => availableEq.Any(eq => eq.Type == e.Type))) continue;
                        }
                    }

                    // Get groups that are not already in the schedule for this day and hour
                    var groupsAvailable = groups.Where(g => !schedule.Any(s =>
                            s.Key.Day == currentDay &&
                            s.Key.TimeSlot == currentHour &&
                            s.Value.Groups.Contains(g.Name)
                    )).ToList();

                    // trouver tous les groupes qui sont sur un autre site pendant le timeslot précédent (si il y en a un, donc pas s'il y a une pause entre 2 timeslots)
                    var previousHour = hours.FirstOrDefault(h => h.endHour == currentHour.startHour);
                    var groupsOnOtherSite = groupsAvailable.Where(g => schedule.Any(s =>
                            s.Key.Day == currentDay &&
                            s.Key.TimeSlot == previousHour &&
                            s.Value.Groups.Contains(g.Name) &&
                            s.Key.Location.Site != currentClass.Site
                        )
                    ).ToList();

                    groupsAvailable = groupsAvailable.Except(groupsOnOtherSite).ToList();

                    List<Groupe> groupsToPlace = new();

                    if (groupsAvailable.Any())
                    {
                        var groupsAvailablePreferingThisSite = groupsAvailable.Where(g => g.PreferedSite == currentClass.Site).ToList();

                        groupsToPlace = BacktrackClassroomsCoursGroups(currentClass.Capacity, groupsAvailablePreferingThisSite);

                        if (groupsToPlace.Sum(g => g.Capacity) < currentClass.Capacity)
                        {
                            var remainingCapacity = currentClass.Capacity - groupsToPlace.Sum(g => g.Capacity);
                            var remainingGroups = groupsAvailable.Except(groupsToPlace).ToList();
                            var groupsAvailableNotPreferingThisSite = BacktrackClassroomsCoursGroups(remainingCapacity, remainingGroups);
                            groupsToPlace.AddRange(groupsAvailableNotPreferingThisSite);
                        }
                    }
                    else continue;

                    if (groupsToPlace.Count == 1)
                    {
                        var isPreferedSiteAnyTimeSlotAvailable = !IsSiteFullForTimeSlot(groupsToPlace[0].PreferedSite, classes, schedule);

                        var preferedSiteEquipments = classes.Where(c => c.Site == groupsToPlace[0].PreferedSite).SelectMany(c => c.Equipments ?? new List<Equipement>());
                        var preferedSiteFlyingEquipments = flyingEquipments.Where(f => f.Site == groupsToPlace[0].PreferedSite).ToList();
                        var preferedSiteAllEquipments = preferedSiteEquipments.Concat(preferedSiteFlyingEquipments).ToList();

                        var havePreferedSiteRequiredEquipments = requiredEquipment is not null && requiredEquipment.Count != 0 ?
                            requiredEquipment.All(e => preferedSiteAllEquipments.Any(f => f.Type == e.Type)) : true;

                        if (currentClass.Site != groupsToPlace[0].PreferedSite)
                        {
                            if (havePreferedSiteRequiredEquipments && isPreferedSiteAnyTimeSlotAvailable) continue;
                        }
                    }

                    var flyingEquipmentsRequired = new List<Equipement>();

                    if (currentRequiredFlyingEquipments is not null && currentRequiredFlyingEquipments.Any())
                    {
                        flyingEquipmentsRequired = availableEq.Where(e => currentRequiredFlyingEquipments.Any(ce => ce.Type == e.Type)).ToList();
                    }

                    var SE = new ScheduleEntry(course, groupsToPlace.Select(g => g.Name).ToList(), flyingEquipmentsRequired);
                    return Tuple.Create(SK, SE);
                }
            }
            return null;
        }

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

        public void DisplaySchedule()
        {
            if (schedule is null || !schedule.Any()) return;

            // sort day, time, site, classroom
            var s = schedule.OrderBy(s => s.Key.Day)
                                .ThenBy(s => s.Key.TimeSlot.StartHour)
                                .ThenBy(s => s.Key.Location.Site)
                                .ThenBy(s => s.Key.Location.Classroom);

            foreach (var item in s)
            {
                // Set site color
                if (item.Key.Location.Site == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (item.Key.Location.Site == "B")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (item.Key.Location.Site == "C")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"Site {item.Key.Location.Site}, classroom {item.Key.Location.Classroom} : {item.Key.Day} {item.Key.TimeSlot.StartHour}-{item.Key.TimeSlot.EndHour} : {item.Value.Course} ({string.Join(",", item.Value.Groups)}) - ({string.Join(",", item.Value.FlyingEquipments.Select(e => e.Type + " " + e.Code))})");

                // Reset color to default
                Console.ResetColor();
            }
        }

        bool IsSiteFullForTimeSlot(
            string site,
            List<Class> classes,
            Schedule schedule
        )
        {
            int maxTimeSlotsForSite = classes
                .Count(c => c.Site == site) * daysOfWeek.Count * hours.Count;

            int usedTimeSlotsForSite = schedule
                .Count(entry => entry.Key.Location.Site == site);

            return usedTimeSlotsForSite >= maxTimeSlotsForSite;
        }
    }
}
