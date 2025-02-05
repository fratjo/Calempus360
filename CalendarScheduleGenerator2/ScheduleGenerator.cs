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
        private readonly List<(string site, string equipment, string code)> flyingEquipments;
        public Schedule schedule { get; private set; } = new Schedule();

        public ScheduleGenerator(
                List<Class> classes,
                List<string> daysOfWeek,
                List<(int startHour, int endHour)> hours,
                List<CourseGroupes> courseGroupes,
                List<(string site, string equipment, string code)> flyingEquipments)
        {
            this.classes = classes;
            this.daysOfWeek = daysOfWeek;
            this.hours = hours;
            this.courseGroupes = courseGroupes;
            this.flyingEquipments = flyingEquipments;
        }

        public Schedule GenerateSchedule()
        {
            // TODO : Add more pre conditions to avoid backtracking if possible, saving ressources

            if ((classes.Count * daysOfWeek.Count * hours.Count) < courseGroupes.Sum(c => c.Groupes.Count)) // If nb slot is enough to place all groups
            {
                throw new Exception("No schedule possible: not enough slots. Nb slots : "
                    + (classes.Count * daysOfWeek.Count * hours.Count)
                    + " Nb groups : " + courseGroupes.Sum(c => c.Groupes.Count));
            }
            else if (courseGroupes.Sum(c => c.GetCapacity()) > (classes.Sum(c => c.Capacity) * daysOfWeek.Count * hours.Count)) // If capacity is enough to place all groups
            {
                throw new Exception("No schedule possible: not enough capacity. Capacity : "
                    + courseGroupes.Sum(c => c.GetCapacity())
                    + " Nb classes : " + classes.Sum(c => c.Capacity) * daysOfWeek.Count * hours.Count);
            }
            else if (!BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule)) // If no schedule found
            {
                throw new Exception("No schedule found");
            }
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
                if (biggestCourseGroup.Equipements is not null && biggestCourseGroup.Equipements.Count > 0) // si j'ai besoin d'équipements
                {
                    if (currentClass.Equipments is not null)
                    {
                        if (!biggestCourseGroup.Equipements.All(currentClass.Equipments.Contains) &&
                            !biggestCourseGroup.Equipements.All(e => !currentClass.Equipments.Contains(e) &&
                                    flyingEquipments.Where(f =>
                                        f.site == currentClass.Site).Any(f =>
                                                f.equipment == e))) continue; // sauf ceux qui sont déjà dans la classe
                    } // si la classe ne possède pas les équipements requis et que les équipements requis ne sont pas tous disponibles sur le site
                    else if (!biggestCourseGroup.Equipements.All(e =>
                                    flyingEquipments.Where(f => f.site == currentClass.Site).Any(f => f.equipment == e))) continue;
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
                    if (schedule.Any(s => s.Key.Day == currentDay && s.Key.TimeSlot == currentHour && s.Value.Course == course)) continue;

                    // var key = ((currentClass.Site, currentClass.Classroom), currentDay, currentHour);
                    var SK = new ScheduleKey((currentClass.Site, currentClass.Classroom), currentDay, currentHour);

                    // Check if the classroom is already in the schedule for this day and hour
                    if (schedule.ContainsKey(SK)) continue;

                    if (requiredEquipment is not null && requiredEquipment.Any())
                    {
                        var fE = flyingEquipments.Where(f => f.site == currentClass.Site).Select(f => f.equipment).ToList();
                        if (currentClass.Equipments is not null)
                        {
                            var notClassEq = requiredEquipment.Where(e => !currentClass.Equipments.Contains(e)).ToList();
                            if (notClassEq.Any())
                            {
                                var hasRequiredEquipment = schedule.Any(s =>
                                    s.Key.Day == currentDay &&
                                    s.Key.TimeSlot == currentHour &&
                                    s.Value.FlyingEquipments.Any(e => notClassEq.Contains(e))
                                );

                                if (hasRequiredEquipment) continue;
                            }
                        }
                        else
                        {
                            var hasRequiredEquipment = schedule.Any(s =>
                                s.Key.Day == currentDay &&
                                s.Key.TimeSlot == currentHour &&
                                s.Value.FlyingEquipments.Any(e => requiredEquipment.Contains(e))
                            );

                            if (hasRequiredEquipment) continue;
                        }
                    }


                    // Get groups that are not already in the schedule for this day and hour
                    var groupsAvailable = groups.Where(g =>
                        !schedule.Any(s =>
                            s.Key.Day == currentDay &&
                            s.Key.TimeSlot == currentHour &&
                            s.Value.Groups.Contains(g.Name)
                    )).ToList();

                    // TODO : Inter site travel time (1h)

                    // trouver tous les groupes qui sont sur un autre site pendant le timeslot précédent
                    var previousHour = hours.FirstOrDefault(h => h.endHour == currentHour.startHour);
                    var groupsOnOtherSite = groupsAvailable.Where(g =>
                        schedule.Any(s =>
                            s.Key.Day == currentDay &&
                            s.Key.TimeSlot == previousHour &&
                            s.Value.Groups.Contains(g.Name) &&
                            s.Key.Location.Site != currentClass.Site
                        )
                    ).ToList();

                    groupsAvailable = groupsAvailable.Except(groupsOnOtherSite).ToList();

                    List<Groupe> groupsToPlace = new();

                    if (groupsAvailable.Count == 1)
                    {
                        var isPeferedSiteAvailable = !IsSiteFullForTimeSlot(groupsAvailable[0].PreferedSite, classes, schedule);
                        var isPreferedSite = groupsAvailable[0].PreferedSite == currentClass.Site;

                        var havePreferedSiteRequiredEquipments = false;
                        var haveCurrentSiteRequiredFlyingEquipments = true;

                        if (requiredEquipment is not null && requiredEquipment.Count != 0)
                        {
                            // est-ce que au moins une de mes classes du site préféré possède les équipements requis
                            havePreferedSiteRequiredEquipments = classes.Any(c =>
                                c.Site == groupsAvailable[0].PreferedSite &&
                                requiredEquipment is not null &&
                                requiredEquipment.Count != 0 &&
                                c.Equipments is not null &&
                                requiredEquipment.All(c.Equipments.Contains));

                            haveCurrentSiteRequiredFlyingEquipments = requiredEquipment is not null && requiredEquipment.Count != 0 ?
                                requiredEquipment.All(e =>
                                    flyingEquipments.Where(f => f.site == currentClass.Site).Any(f => f.equipment == e)) : true;
                        }

                        if ((isPeferedSiteAvailable && isPreferedSite) ||
                            (!isPeferedSiteAvailable && !isPreferedSite) ||
                            (isPeferedSiteAvailable && !isPreferedSite && !havePreferedSiteRequiredEquipments) ||
                            (isPeferedSiteAvailable && !isPreferedSite && !havePreferedSiteRequiredEquipments && !haveCurrentSiteRequiredFlyingEquipments))
                        {
                            groupsToPlace = groupsAvailable;
                        }
                    }
                    else if (groupsAvailable.Any())
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

                    if (groupsToPlace.Count > 0)
                    {
                        var flyingEquipmentsRequired = new List<string>();

                        if (requiredEquipment is not null && requiredEquipment.Count != 0)
                        {
                            if (currentClass.Equipments is not null)
                            {
                                // on prend les équipements requis que la classe ne possède pas
                                flyingEquipmentsRequired = requiredEquipment.Where(e =>
                                    !currentClass.Equipments.Contains(e) &&
                                    flyingEquipments.Where(f => f.site == currentClass.Site).Any(f => f.equipment == e)
                                ).ToList();
                            }
                            else
                            {
                                // on prend tous les équipements requis
                                flyingEquipmentsRequired = requiredEquipment.Where(e =>
                                    flyingEquipments.Where(f => f.site == currentClass.Site).Any(f => f.equipment == e)
                                ).ToList();
                            }

                        }

                        var SE = new ScheduleEntry(course, groupsToPlace.Select(g => g.Name).ToList(), flyingEquipmentsRequired);
                        return Tuple.Create(SK, SE);
                    }
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

        public void DisplaySchedule(Schedule schedule)
        {
            if (schedule is null) return;
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
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine($"Site {item.Key.Location.Site}, classroom {item.Key.Location.Classroom} : {item.Key.Day} {item.Key.TimeSlot.StartHour}-{item.Key.TimeSlot.EndHour} : {item.Value.Course} ({string.Join(",", item.Value.Groups)}) - ({string.Join(",", item.Value.FlyingEquipments)})");

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
