namespace ScheduleGenerator
{
    public class ScheduleGenerator
    {
        private readonly List<Class> classes;
        private readonly List<string> daysOfWeek;
        private readonly List<(int startHour, int endHour)> hours;
        private readonly List<CourseGroups> courseGroups;
        private readonly List<Equipement> flyingEquipments;
        public Schedule schedule { get; private set; } = new Schedule();

        public ScheduleGenerator(
                List<Class> classes,
                List<string> daysOfWeek,
                List<(int startHour, int endHour)> hours,
                List<CourseGroups> courseGroups,
                List<Equipement> flyingEquipments)
        {
            this.classes = classes;
            this.daysOfWeek = daysOfWeek;
            this.hours = hours;
            this.courseGroups = courseGroups;
            this.flyingEquipments = flyingEquipments;
        }

        public Schedule GenerateSchedule()
        {
            System.Console.WriteLine("Generating schedule...");
            var timeSlots = classes.Count * daysOfWeek.Count * hours.Count;
            System.Console.WriteLine("Time slots (reserve: 5%) : " + (timeSlots - timeSlots * 0.05));
            var groupsCount = courseGroups.Sum(c => c.Groups.Count);
            System.Console.WriteLine("Groups count : " + groupsCount);
            var classesCapacity = classes.Sum(c => c.Capacity) * daysOfWeek.Count * hours.Count;
            System.Console.WriteLine("Capacity (reserve: 5%): " + (classesCapacity - (classesCapacity * 0.05)));
            var groupsCapacity = courseGroups.Sum(c => c.GetCapacity());
            System.Console.WriteLine("Groups capacity: " + groupsCapacity);

            // -------------------------------------------------------------------------------------- //
            // Pre conditions : Check if there is enough time slots and capacity to place all groups  // 
            // -------------------------------------------------------------------------------------- //
            if ((timeSlots - timeSlots * 0.05) < groupsCount) // If nb slot is enough to place all groups
            {
                throw new Exception("No schedule possible: not enough slots.");
            }
            else if (classesCapacity - (classesCapacity * 0.05) < groupsCapacity) // If capacity is enough to place all groups
            {
                throw new Exception("No schedule possible: not enough capacity.");
            }
            else if (!BacktrackSchedule(daysOfWeek, hours, courseGroups, classes, schedule)) // If no schedule found
            {
                throw new Exception("No schedule found");
            }

            System.Console.WriteLine("Schedule generated");

            return schedule;
        }

        private bool BacktrackSchedule(
            List<string> daysOfWeek,
            List<(int startHour, int endHour)> hours,
            List<CourseGroups> courseGroups,
            List<Class> classes,
            Schedule schedule)
        {
            if (courseGroups.Count == 0 || classes.Count == 0) return true;

            courseGroups = courseGroups.OrderByDescending(c => c.GetEquipmentCount()).ThenByDescending(c => c.GetCapacity()).ToList();
            classes = classes.OrderByDescending(c => c.Capacity).ToList();


            var biggestCourseGroup = courseGroups[0];
            var biggestCourse = biggestCourseGroup.Course;
            int courseCapacity = biggestCourseGroup.GetCapacity();

            foreach (var currentClass in classes)
            {
                // ----------------------------------------------------------------------------------- //
                // Constraint: check if the required equipment are available in the classroom + flying //
                // ----------------------------------------------------------------------------------- //
                if (!CheckIfSiteContainsAllRequiredEquipment(biggestCourseGroup, currentClass)) continue;

                var keyAndGroups = FindTimeSlotForCourseGroup(
                    currentClass,
                    biggestCourseGroup, schedule);
                if (keyAndGroups is not null)
                {
                    var key = keyAndGroups.Value.Item1;
                    var entry = keyAndGroups.Value.Item2;

                    if (entry.Groups.Count > 0)
                    {
                        schedule.Add(key, entry);

                        biggestCourseGroup.Groups.RemoveAll(g => entry.Groups.Contains(g.Name));
                        if (biggestCourseGroup.Groups.Count == 0) courseGroups.RemoveAt(0);

                        var success = BacktrackSchedule(daysOfWeek, hours, courseGroups, classes, schedule);
                        if (success) return true;

                        schedule.Remove(key);
                        biggestCourseGroup.Groups.AddRange(entry.Groups.Select(name => new Group { Name = name }));
                        courseGroups.Insert(0, biggestCourseGroup);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Check if the site contains all the required equipment for the course group
        /// </summary>
        /// <param name="biggestCourseGroup"></param>
        /// <param name="currentClass"></param>
        /// <returns>true if contains all, false if at least one is missing</returns>
        private bool CheckIfSiteContainsAllRequiredEquipment(CourseGroups biggestCourseGroup, Class currentClass)
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
                        return false;
                }
                else if (!allInFlyingEquipments) return false;

            }

            return true;
        }

        /// <summary>
        /// Find a time slot for a course group in a classroom with the equipment required.
        /// </summary>
        /// <param name="currentClass"></param>
        /// <param name="courseGroup"></param>
        /// <param name="schedule"></param>
        /// <returns>A tuple (ScheduleKey, ScheduleEntry) if find, null if not find</returns>
        private (ScheduleKey, ScheduleEntry)? FindTimeSlotForCourseGroup(Class currentClass, CourseGroups courseGroup, Schedule schedule)
        {
            var groups = courseGroup.Groups;
            var course = courseGroup.Course;
            var requiredEquipment = courseGroup.Equipements;

            foreach (var currentDay in daysOfWeek)
            {
                // ------------------------------------------------------------- //
                // Constraint: Not more than 2h of a course per day for a groupe //
                // ------------------------------------------------------------- //
                CheckNoMoreThan2HoursPerDayPerGroupForACourse(schedule, groups, course, currentDay);

                foreach (var timeSlot in hours)
                {
                    var scheduleKey = new ScheduleKey((currentClass.Site, currentClass.Name), currentDay, timeSlot);

                    // ------------------------------------------------------------ //
                    // Constraint: Check if the class is already taken at this time //
                    // ------------------------------------------------------------ //
                    if (schedule.ContainsKey(scheduleKey)) continue;

                    // --------------------------------------------------------- //
                    // Constraint: Can place 2 slots of the same course in a row //
                    // --------------------------------------------------------- //
                    if (!CheckIfCanPlace2SameCourseInARow(
                        currentClass, schedule, groups, course, currentDay, timeSlot, scheduleKey,
                        out (int startHour, int endHour) previousHour,
                        out (int startHour, int endHour) nextHour,
                        out (ScheduleKey, ScheduleEntry)? value))
                        return value;


                    // -------------------------------------------------------------------------- //
                    // Constraint: Check if the required equipment are available for the timeslot //
                    // -------------------------------------------------------------------------- //
                    if (!CheckIfRequirementEquipementAreAvailableForTheTimeSlot(
                        currentClass, schedule, requiredEquipment, currentDay, timeSlot,
                        out List<Equipement>? currentRequiredFlyingEquipments, out List<Equipement> availableEq)) continue;

                    // -------------------------------------------------------------- //
                    // Constraint: Check if the groups are available for the timeslot //
                    // -------------------------------------------------------------- //
                    CheckGroupsAvailableForCurretTimeSlot(schedule, groups, currentDay, timeSlot, out List<Group> groupsAvailable);

                    // -------------------------------------------------------------------------------------- //
                    // Constraint: Check if the groups are not on an other site for the previous or next hour //
                    // -------------------------------------------------------------------------------------- //
                    groupsAvailable = CheckGroupNotOnAOtherSiteForPreviousOrNextTimeSlot(currentClass, schedule, currentDay, previousHour, nextHour, groupsAvailable);


                    // ----------------------------------------------------------------- //
                    // Constraint: Check the groups that can be placed in this classroom //
                    // ----------------------------------------------------------------- //
                    if (!CheckIfAnyGroupToPlaceAndPlaceIt(currentClass, groupsAvailable, out List<Group> groupsToPlace)) continue;


                    // ----------------------------------------------------------------------------------------------- //
                    // Constraint: Check if the prefered site of the groups is available & have the required equipment //
                    // ----------------------------------------------------------------------------------------------- //
                    if (!CheckIfAllTheGroupsPreferTheSameSite(currentClass, schedule, requiredEquipment, groupsToPlace)) continue;

                    // -------------------------------------------------------- //
                    // Constraint: Place the equipments required for the course //
                    // -------------------------------------------------------- //
                    var flyingEquipmentsRequired = new List<Equipement>();

                    if (currentRequiredFlyingEquipments is not null && currentRequiredFlyingEquipments.Any())
                    {
                        flyingEquipmentsRequired = availableEq.Where(e => currentRequiredFlyingEquipments.Any(ce => ce.Type == e.Type)).ToList();
                    }

                    // ------------------------- //
                    // Create the schedule entry //
                    // ------------------------- //
                    var SE = new ScheduleEntry(course, groupsToPlace.Select(g => g.Name).ToList(), flyingEquipmentsRequired);
                    return (scheduleKey, SE);
                }
            }
            return null;
        }

        /// <summary>
        /// Check if we can place 2 slots of the same course in a row
        /// </summary>
        /// <param name="currentClass"></param>
        /// <param name="schedule"></param>
        /// <param name="groups"></param>
        /// <param name="course"></param>
        /// <param name="currentDay"></param>
        /// <param name="timeSlot"></param>
        /// <param name="scheduleKey"></param>
        /// <param name="previousHour"></param>
        /// <param name="nextHour"></param>
        /// <param name="value"></param>
        /// <returns>true if we must continue to place, false if we have a new schedule entry</returns>
        private bool CheckIfCanPlace2SameCourseInARow(Class currentClass, Schedule schedule, List<Group> groups, string course, string currentDay, (int startHour, int endHour) timeSlot, ScheduleKey scheduleKey, out (int startHour, int endHour) previousHour, out (int startHour, int endHour) nextHour, out (ScheduleKey, ScheduleEntry)? value)
        {
            previousHour = hours.FirstOrDefault(h => h.endHour == timeSlot.startHour);
            var pvH = previousHour;
            var previousCourseInSameClassroom = schedule.FirstOrDefault(s =>
                                    s.Key.Day == currentDay &&
                                    IsSameTimeSlot(s.Key.TimeSlot, pvH) &&
                                    s.Key.Location.Site == currentClass.Site &&
                                    s.Key.Location.Classroom == currentClass.Name
                                );
            var previousGroupsInSameClassroom = previousCourseInSameClassroom.Value?.Groups ?? new List<string>();

            nextHour = hours.FirstOrDefault(h => h.startHour == timeSlot.endHour);
            var nxH = nextHour;
            var nextCourseInSameClassroom = schedule.FirstOrDefault(s =>
                s.Key.Day == currentDay &&
                IsSameTimeSlot(s.Key.TimeSlot, nxH) &&
                s.Key.Location.Site == currentClass.Site &&
                s.Key.Location.Classroom == currentClass.Name
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
                        previousCourseInSameClassroom.Key.Location.Classroom == currentClass.Name &&
                        previousGroupsInSameClassroom.All(g => groups.Any(go => go.Name == g)) &&
                        schedule.Count(s => s.Key.Day == currentDay && s.Value.Course == course) < 2)
                    {
                        var newEntry = new ScheduleEntry(course, previousGroupsInSameClassroom, previousCourseInSameClassroom.Value.FlyingEquipments);
                        value = (scheduleKey, newEntry);
                        return false;
                    }
                }
                else if (nextCourseInSameClassroom.Key is not null && schedule.ContainsKey(nextCourseInSameClassroom.Key))
                {
                    if (nextCourseInSameClassroom.Value!.Course == course &&
                        nextCourseInSameClassroom.Key.Location.Site == currentClass.Site &&
                        nextCourseInSameClassroom.Key.Location.Classroom == currentClass.Name &&
                        nextGroupsInSameClassroom.All(g => groups.Any(go => go.Name == g)) &&
                        schedule.Count(s => s.Key.Day == currentDay && s.Value.Course == course) < 2)
                    {
                        var newEntry = new ScheduleEntry(course, nextGroupsInSameClassroom, nextCourseInSameClassroom.Value.FlyingEquipments);
                        value = (scheduleKey, newEntry);
                        return false;
                    }
                }
            }

            value = null;
            return true;
        }

        /// <summary>
        /// Check if the required equipment are available in the classroom + flying, for the current time slot
        /// </summary>
        /// <param name="currentClass"></param>
        /// <param name="schedule"></param>
        /// <param name="requiredEquipment"></param>
        /// <param name="currentDay"></param>
        /// <param name="timeSlot"></param>
        /// <param name="currentRequiredFlyingEquipments"></param>
        /// <param name="availableEq"></param>
        /// <returns>true if all required equipmenet are available, false if a least one is missing</returns>
        private bool CheckIfRequirementEquipementAreAvailableForTheTimeSlot(Class currentClass, Schedule schedule, List<Equipement>? requiredEquipment, string currentDay, (int startHour, int endHour) timeSlot, out List<Equipement>? currentRequiredFlyingEquipments, out List<Equipement> availableEq)
        {
            var currentSiteFlyingEquipments = flyingEquipments.Where(f => f.Site == currentClass.Site).Select(f => f).ToList();

            List<Equipement> takenEq = schedule.Where(s =>
                                                s.Key.Day == currentDay &&
                                                IsSameTimeSlot(s.Key.TimeSlot, timeSlot) &&
                                                s.Key.Location.Site == currentClass.Site).SelectMany(s => s.Value.FlyingEquipments).ToList();

            availableEq = currentSiteFlyingEquipments.Where(e => !takenEq.Any(eq => eq.Code == e.Code)).ToList();

            var aEq = availableEq;

            currentRequiredFlyingEquipments = new();

            if (requiredEquipment is not null && requiredEquipment.Any())
            {
                if (currentClass.Equipments is not null && currentClass.Equipments.Any())
                {
                    currentRequiredFlyingEquipments = requiredEquipment.Where(e => !currentClass.Equipments.Any(ce => e.Type == ce.Type)).ToList();

                    if (currentRequiredFlyingEquipments.Any())
                    {
                        if (!currentRequiredFlyingEquipments.All(e => aEq.Any(eq => eq.Type == e.Type))) return false;
                    }
                }
                else
                {
                    if (!requiredEquipment.All(e => aEq.Any(eq => eq.Type == e.Type))) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if all the groups prefer the same site and if the site + equipment is available for the current time slot
        /// </summary>
        /// <param name="currentClass"></param>
        /// <param name="schedule"></param>
        /// <param name="requiredEquipment"></param>
        /// <param name="groupsToPlace"></param>
        /// <returns>true if we can place the groups on current site, false if we must place groups on the prefered site</returns>
        private bool CheckIfAllTheGroupsPreferTheSameSite(Class currentClass, Schedule schedule, List<Equipement>? requiredEquipment, List<Group> groupsToPlace)
        {
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
                    if (havePreferedSiteRequiredEquipments && isPreferedSiteAnyTimeSlotAvailable) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check if any group can be placed in the classroom, and place them into groupsToPlace
        /// </summary>
        /// <param name="currentClass"></param>
        /// <param name="groupsAvailable"></param>
        /// <param name="groupsToPlace"></param>
        /// <returns>true if any group have been placed, false if no group have been placed</returns>
        private bool CheckIfAnyGroupToPlaceAndPlaceIt(Class currentClass, List<Group> groupsAvailable, out List<Group> groupsToPlace)
        {
            groupsToPlace = new();

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
            else return false;
            return true;
        }

        /// <summary>
        /// Check if the groups are not on an other site for the previous or next hour
        /// </summary>
        /// <param name="currentClass"></param>
        /// <param name="schedule"></param>
        /// <param name="currentDay"></param>
        /// <param name="previousHour"></param>
        /// <param name="nextHour"></param>
        /// <param name="groupsAvailable"></param>
        /// <returns>List of groups that are available and not on a other ste for previous and next timeSlot</returns>
        private List<Group> CheckGroupNotOnAOtherSiteForPreviousOrNextTimeSlot(Class currentClass, Schedule schedule, string currentDay, (int startHour, int endHour) previousHour, (int startHour, int endHour) nextHour, List<Group> groupsAvailable)
        {
            if (previousHour.startHour is not default(int) && previousHour.endHour is not default(int))
            {
                var groupsOnOtherSite = groupsAvailable.Where(g => schedule.Any(s =>
                        s.Key.Day == currentDay &&
                        IsSameTimeSlot(s.Key.TimeSlot, previousHour) &&
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
                        IsSameTimeSlot(s.Key.TimeSlot, nextHour) &&
                        s.Value.Groups.Contains(g.Name) &&
                        s.Key.Location.Site != currentClass.Site
                    )
                ).ToList();

                groupsAvailable = groupsAvailable.Where(g => !groupsOnOtherSite.Any(go => go.Name == g.Name)).ToList();
            }

            return groupsAvailable;
        }

        /// <summary>
        /// Check if the groups are available for the current time slot
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="groups"></param>
        /// <param name="currentDay"></param>
        /// <param name="timeSlot"></param>
        /// <returns>List of groups that are availabe for the time slot</returns>
        private void CheckGroupsAvailableForCurretTimeSlot(Schedule schedule, List<Group> groups, string currentDay, (int startHour, int endHour) timeSlot, out List<Group> groupsAvailable)
        {
            groupsAvailable = groups.Where(g => !schedule.Any(s =>
                    s.Key.Day == currentDay &&
                    IsSameTimeSlot(s.Key.TimeSlot, timeSlot) &&
                    s.Value.Groups.Contains(g.Name)
            )).ToList();
        }

        /// <summary>
        /// Check if a group has more than 2 hours of a course per day, and remove it from the list of groups
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="groups"></param>
        /// <param name="course"></param>
        /// <param name="currentDay"></param>
        private static void CheckNoMoreThan2HoursPerDayPerGroupForACourse(Schedule schedule, List<Group> groups, string course, string currentDay)
        {
            var groupsToRemove = groups.Where(g =>
                                schedule.Count(s => s.Key.Day == currentDay && s.Value.Groups.Contains(g.Name) && s.Value.Course == course) >= 2).ToList();
            groups.RemoveAll(g => groupsToRemove.Contains(g));
        }

        /// <summary>
        /// Backtrack algorithm to find the best combinaison of groups to place in a classroom
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="groups"></param>
        /// <returns>List of groups</returns>
        private List<Group> BacktrackClassroomsCoursGroups(int capacity, List<Group> groups)
        {
            // memoization
            List<Group> bestCombinaison = new();
            List<Group> currentCombinaison = new();
            int bestCombinaisonCapacity = 0;

            void Backtrack(int index, int currentSum)
            {
                if (currentSum <= capacity && currentSum > bestCombinaisonCapacity)
                {
                    bestCombinaisonCapacity = currentSum;
                    bestCombinaison = [.. currentCombinaison];
                }


                for (int i = index; i < groups.Count; i++)
                {
                    if (currentSum + groups[i].Capacity <= capacity)
                    {
                        currentCombinaison.Add(groups[i]);

                        Backtrack(i + 1, currentSum + groups[i].Capacity);

                        currentCombinaison.RemoveAt(currentCombinaison.Count - 1);
                    }
                }
            }

            Backtrack(0, 0);

            return bestCombinaison;
        }

        /// <summary>
        /// Check if two time slots are the same
        /// </summary>
        /// <param name="slot1"></param>
        /// <param name="slot2"></param>
        /// <returns>true if time slots are the same, false if time slots are not the same</returns>
        private bool IsSameTimeSlot((int startHour, int endHour) slot1, (int startHour, int endHour) slot2)
        {
            return slot1.startHour == slot2.startHour && slot1.endHour == slot2.endHour;
        }

        /// <summary>
        /// Check if the site is full for the current time slot
        /// </summary>
        /// <param name="site"></param>
        /// <param name="classes"></param>
        /// <param name="schedule"></param>
        /// <returns>true if site is full, false if site is available</returns>
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
