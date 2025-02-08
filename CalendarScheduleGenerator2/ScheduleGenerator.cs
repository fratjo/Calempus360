using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarScheduleGenerator2
{
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
                    }
                    else if (!allInFlyingEquipments) continue;

                }

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

        private Tuple<ScheduleKey, ScheduleEntry>? FindTimeSlotForCourseGroup(
            Class currentClass,
            CourseGroupes courseGroup,
            Schedule schedule
        )
        {
            var groups = courseGroup.Groupes;
            var course = courseGroup.Course;
            var requiredEquipment = courseGroup.Equipements;

            foreach (var currentDay in daysOfWeek)
            {
                groups.ForEach(g =>
                {
                    var count = schedule.Count(s => s.Key.Day == currentDay && s.Value.Groups.Contains(g.Name) && s.Value.Course == course);
                    if (count >= 2)
                    {
                        groups.Remove(g);
                    }
                });

                foreach (var currentHour in hours)
                {
                    var SK = new ScheduleKey((currentClass.Site, currentClass.Classroom), currentDay, currentHour);

                    if (schedule.ContainsKey(SK)) continue;

                    var previousHour = hours.FirstOrDefault(h => h.endHour == currentHour.startHour);
                    var previousCourseInSameClassroom = schedule.FirstOrDefault(s =>
                        s.Key.Day == currentDay &&
                        s.Key.TimeSlot.StartHour == previousHour.startHour &&
                        s.Key.TimeSlot.EndHour == previousHour.endHour &&
                        s.Key.Location.Site == currentClass.Site &&
                        s.Key.Location.Classroom == currentClass.Classroom
                    );
                    var previousGroupsInSameClassroom = previousCourseInSameClassroom.Value?.Groups ?? new List<string>();

                    var nextHour = hours.FirstOrDefault(h => h.startHour == currentHour.endHour);
                    var nextCourseInSameClassroom = schedule.FirstOrDefault(s =>
                        s.Key.Day == currentDay &&
                        s.Key.TimeSlot.StartHour == nextHour.startHour &&
                        s.Key.TimeSlot.EndHour == nextHour.endHour &&
                        s.Key.Location.Site == currentClass.Site &&
                        s.Key.Location.Classroom == currentClass.Classroom
                    );
                    var nextGroupsInSameClassroom = nextCourseInSameClassroom.Value?.Groups ?? new List<string>();

                    if ((previousCourseInSameClassroom.Key is not null &&
                        schedule.ContainsKey(previousCourseInSameClassroom.Key) &&
                        nextCourseInSameClassroom.Key is not null &&
                        !schedule.ContainsKey(nextCourseInSameClassroom.Key))
                        ||
                        (previousCourseInSameClassroom.Key is not null &&
                        !schedule.ContainsKey(previousCourseInSameClassroom.Key) &&
                        nextCourseInSameClassroom.Key is not null &&
                        schedule.ContainsKey(nextCourseInSameClassroom.Key)))
                    {
                        if (previousCourseInSameClassroom.Key is not null && schedule.ContainsKey(previousCourseInSameClassroom.Key))
                        {
                            if (previousCourseInSameClassroom.Value!.Course == course &&
                                previousCourseInSameClassroom.Key.Location.Site == currentClass.Site &&
                                previousCourseInSameClassroom.Key.Location.Classroom == currentClass.Classroom &&
                                previousGroupsInSameClassroom.All(g => groups.Any(go => go.Name == g)) &&
                                schedule.Count(s => s.Key.Day == currentDay && s.Value.Course == course) < 2)
                            {
                                var newEntry = new ScheduleEntry(course, previousGroupsInSameClassroom, previousCourseInSameClassroom.Value.FlyingEquipments);
                                return Tuple.Create(SK, newEntry);
                            }
                        }
                        else if (nextCourseInSameClassroom.Key is not null && schedule.ContainsKey(nextCourseInSameClassroom.Key))
                        {
                            if (nextCourseInSameClassroom.Value!.Course == course &&
                                nextCourseInSameClassroom.Key.Location.Site == currentClass.Site &&
                                nextCourseInSameClassroom.Key.Location.Classroom == currentClass.Classroom &&
                                nextGroupsInSameClassroom.All(g => groups.Any(go => go.Name == g)) &&
                                schedule.Count(s => s.Key.Day == currentDay && s.Value.Course == course) < 2)
                            {
                                var newEntry = new ScheduleEntry(course, nextGroupsInSameClassroom, nextCourseInSameClassroom.Value.FlyingEquipments);
                                return Tuple.Create(SK, newEntry);
                            }
                        }
                    }

                    var currentSiteFlyingEquipments = flyingEquipments.Where(f => f.Site == currentClass.Site).Select(f => f).ToList();

                    List<Equipement> takenEq = schedule.Where(s =>
                                                        s.Key.Day == currentDay &&
                                                        s.Key.TimeSlot == currentHour &&
                                                        s.Key.Location.Site == currentClass.Site).SelectMany(s => s.Value.FlyingEquipments).ToList();

                    var availableEq = currentSiteFlyingEquipments.Where(e => !takenEq.Any(eq => eq.Code == e.Code)).ToList();

                    var currentRequiredFlyingEquipments = requiredEquipment;

                    if (requiredEquipment is not null && requiredEquipment.Any())
                    {
                        if (currentClass.Equipments is not null && currentClass.Equipments.Any())
                        {
                            currentRequiredFlyingEquipments = requiredEquipment.Where(e => !currentClass.Equipments.Any(ce => e.Type == ce.Type)).ToList();

                            if (currentRequiredFlyingEquipments.Any())
                            {
                                if (!currentRequiredFlyingEquipments.All(e => availableEq.Any(eq => eq.Type == e.Type))) continue;
                            }
                        }
                        else
                        {
                            if (!requiredEquipment.All(e => availableEq.Any(eq => eq.Type == e.Type))) continue;
                        }
                    }

                    var groupsAvailable = groups.Where(g => !schedule.Any(s =>
                            s.Key.Day == currentDay &&
                            s.Key.TimeSlot == currentHour &&
                            s.Value.Groups.Contains(g.Name)
                    )).ToList();

                    if (previousHour.startHour is not default(int) && previousHour.endHour is not default(int))
                    {
                        var groupsOnOtherSite = groupsAvailable.Where(g => schedule.Any(s =>
                                s.Key.Day == currentDay &&
                                s.Key.TimeSlot.StartHour == previousHour.startHour &&
                                s.Key.TimeSlot.EndHour == previousHour.endHour &&
                                s.Value.Groups.Contains(g.Name) &&
                                s.Key.Location.Site != currentClass.Site
                            )
                        ).ToList();

                        groupsAvailable = groupsAvailable.Where(g => !groupsOnOtherSite.Any(go => go.Name == g.Name)).ToList();
                    }

                    if (nextHour.startHour is not default(int) && nextHour.endHour is not default(int))
                    {
                        var groupsOnOtherSite = groupsAvailable.Where(g => schedule.Any(s =>
                                s.Key.Day == currentDay &&
                                s.Key.TimeSlot.StartHour == nextHour.startHour &&
                                s.Key.TimeSlot.EndHour == nextHour.endHour &&
                                s.Value.Groups.Contains(g.Name) &&
                                s.Key.Location.Site != currentClass.Site
                            )
                        ).ToList();

                        groupsAvailable = groupsAvailable.Where(g => !groupsOnOtherSite.Any(go => go.Name == g.Name)).ToList();
                    }

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

                    if (groupsToPlace.Count == 1 || groupsToPlace.All(g => g.PreferedSite == groupsToPlace[0].PreferedSite))
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

        private List<Groupe> BacktrackClassroomsCoursGroups(int capacity, List<Groupe> groupes)
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


        private bool IsSiteFullForTimeSlot(string site, List<Class> classes, Schedule schedule)
        {
            int maxTimeSlotsForSite = classes
                .Count(c => c.Site == site) * daysOfWeek.Count * hours.Count;

            int usedTimeSlotsForSite = schedule
                .Count(entry => entry.Key.Location.Site == site);

            return usedTimeSlotsForSite >= maxTimeSlotsForSite;
        }

        public void DisplaySchedule()
        {
            if (schedule is null || !schedule.Any()) return;

            var s = schedule.OrderBy(s => s.Key.Day)
                                .ThenBy(s => s.Key.TimeSlot.StartHour)
                                .ThenBy(s => s.Key.Location.Site)
                                .ThenBy(s => s.Key.Location.Classroom);

            foreach (var item in s)
            {
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

                Console.ResetColor();
            }
        }
    }
}
