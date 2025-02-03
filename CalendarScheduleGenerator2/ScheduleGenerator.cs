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

        public Dictionary<
            ((string site, string classroom) location,
            string day,
            (int startHour, int endHour) timeSlot),
            (string course, List<string> groups, List<string> flyingEquipments)> GenerateSchedule()
        {
            Dictionary<
                ((string site, string classroom) location,
                string day,
                (int startHour, int endHour) timeSlot),
                (string course, List<string> groups, List<string> flyingEquipments)> schedule = new();

            // TODO : Add more pre conditions to avoid backtracking if possible, saving ressources

            if ((classes.Count * daysOfWeek.Count * hours.Count - classes.Count * daysOfWeek.Count * hours.Count * 0.1) >= courseGroupes.Sum(c => c.Groupes.Count)) // If nb slot is enough to place all groups
            {
                if (BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, 0)) return schedule;
            }
            else if (courseGroupes.Sum(c => c.GetCapacity()) <= (classes.Sum(c => c.Capacity) * daysOfWeek.Count * hours.Count - classes.Count * daysOfWeek.Count * hours.Count * 0.1)) // If capacity is enough to place all groups
            {
                if (BacktrackSchedule(daysOfWeek, hours, courseGroupes, classes, schedule, 0)) return schedule;
            }
            else throw new Exception("No schedule possible");

            System.Console.WriteLine("No schedule found");

            return schedule;
        }

        private bool BacktrackSchedule(
            List<string> daysOfWeek,
            List<(int startHour, int endHour)> hours,
            List<CourseGroupes> courseGroupes,
            List<Class> classes,
            Dictionary<
                    ((string site, string classroom) location,
                    string day,
                    (int startHour, int endHour) timeSlot),
                    (string course, List<string> groups, List<string> flyingEquipments)> schedule,
            int index)
        {
            if (courseGroupes.Count == 0 || classes.Count == 0) return true;

            courseGroupes = courseGroupes.OrderByDescending(c => c.GetEquipmentCount()).ThenByDescending(c => c.GetCapacity()).ToList();
            classes = classes.OrderByDescending(c => c.Capacity).ToList();

            var biggestCourseGroup = courseGroupes[0];
            var biggestCourse = biggestCourseGroup.Course;
            int courseCapacity = biggestCourseGroup.GetCapacity();

            foreach (var currentClass in classes)
            {
                if (biggestCourseGroup.Equipements is not null && biggestCourseGroup.Equipements.Count > 0)
                {
                    if (currentClass.Equipments is null) continue;
                    if (!biggestCourseGroup.Equipements.All(e => currentClass.Equipments.Contains(e)) &&
                        !biggestCourseGroup.Equipements.All(e =>
                                flyingEquipments.Where(f => f.site == currentClass.Site).Any(f => f.equipment == e))) continue;
                }

                var keyAndGroups = FindTimeSlotForCourseGroup(
                    currentClass,
                    biggestCourseGroup, schedule);

                if (keyAndGroups is not null)
                {
                    var (key, availableGroups, flyingEquipments) = keyAndGroups;

                    if (availableGroups.Count > 0)
                    {
                        schedule.Add(key, (biggestCourse, availableGroups, flyingEquipments));

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

        Tuple<((string site, string classroom) location, string day, (int startHour, int endHour) timeSlot), List<string>, List<string>>? FindTimeSlotForCourseGroup(
            Class currentClass,
            CourseGroupes courseGroup,
            Dictionary<
                ((string site, string classroom) location,
                string day,
                (int startHour, int endHour) timeSlot),
            (string course, List<string> groups, List<string> flyingEquipments)> schedule
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

                    var key = ((currentClass.Site, currentClass.Classroom), currentDay, currentHour);

                    // Check if the classroom is already in the schedule for this day and hour
                    if (schedule.ContainsKey(key)) continue;

                    // TODO : Inter site travel time (1h)

                    // TODO : Check if flying equipment is available for this time slot
                    if (requiredEquipment is not null && requiredEquipment.Any())
                    {
                        var fE = flyingEquipments.Where(f => f.site == currentClass.Site).Select(f => f.equipment).ToList();
                        if (currentClass.Equipments is not null)
                        {
                            var notClassEq = requiredEquipment.Where(e => !currentClass.Equipments.Contains(e)).ToList();
                            if (notClassEq.Any())
                            {
                                var hasRequiredEquipment = schedule.Any(s =>
                                    s.Key.day == currentDay &&
                                    s.Key.timeSlot == currentHour &&
                                    s.Value.flyingEquipments.Any(e => notClassEq.Contains(e))
                                );

                                if (hasRequiredEquipment) continue;
                            }
                        }
                        else
                        {
                            var hasRequiredEquipment = schedule.Any(s =>
                                s.Key.day == currentDay &&
                                s.Key.timeSlot == currentHour &&
                                s.Value.flyingEquipments.Any(e => requiredEquipment.Contains(e))
                            );

                            if (hasRequiredEquipment) continue;
                        }
                    }


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
                                requiredEquipment.All(e => c.Equipments.Contains(e)));

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

                        return Tuple.Create(key, groupsToPlace.Select(g => g.Name).ToList(), flyingEquipmentsRequired);
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

        public void DisplaySchedule(Dictionary<((string site, string classroom) location, string day, (int startHour, int endHour) timeSlot), (string course, List<string> groups, List<string> flyingEquipments)> schedule)
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

                Console.WriteLine($"Site {item.Key.location.site}, classroom {item.Key.location.classroom} : {item.Key.day} {item.Key.timeSlot.startHour}-{item.Key.timeSlot.endHour} : {item.Value.course} ({string.Join(",", item.Value.groups)}) - ({string.Join(",", item.Value.flyingEquipments)})");

                // Reset color to default
                Console.ResetColor();
            }
        }

        bool IsSiteFullForTimeSlot(
            string site,
            List<Class> classes,
            Dictionary<
                ((string site, string classroom) location,
                string day,
                (int startHour, int endHour) timeSlot),
            (string course, List<string> groups, List<string> flyingEquipments)> schedule
        )
        {
            int maxTimeSlotsForSite = classes
                .Count(c => c.Site == site) * daysOfWeek.Count * hours.Count;

            int usedTimeSlotsForSite = schedule
                .Count(entry => entry.Key.location.site == site);

            return usedTimeSlotsForSite >= maxTimeSlotsForSite;
        }
    }
}
