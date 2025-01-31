// class X
// {

//     bool BacktrackScheduleBis(
//         List<string> daysOfWeek,
//         List<(int startHour, int endHour)> hours,
//         List<CourseGroupes> courseGroupes,
//         List<(string classroom, string site, int capacity)> classes,
//         Dictionary<
//             Tuple<
//                 ((string site, string classroom) location,
//                 string day,
//                 (int startHour, int endHour) timeSlot)>,
//             (string course, List<string> groups)> schedule,
//         int currentCourseIndex)
//     {
//         // si on a plus de cours à placer, on retourne true
//         if (courseGroupes.Count == 0) return true;

//         // trier les cours par capacité
//         courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();

//         // trier les classes par capacité
//         classes = classes.OrderByDescending(c => c.Item3).ToList();

//         // on prend le cours le plus gros, le premier car trié par capacité
//         string currentCourse = courseGroupes[0].Course;


//         // pour chaque classe (ordonné par capacité)
//         for (int c = 0; c < classes.Count; c++)
//         {
//             var currentClass = classes[c];

//             // TODO : Equipement in the classroom
//             // TODO : Flying Equipment available

//             // si la capacité de la classe est suffisante
//             if (currentClass.Item3 >= courseGroupes[currentCourseIndex].GetCapacity())
//             {
//                 //! Create specific function for this part
//                 // on trouve un timeslot de libre pour la classe
//                 for (int d = 0; d < daysOfWeek.Count; d++)
//                 {
//                     var currentDay = daysOfWeek[d];

//                     // pour chaque timeslot de la jourée
//                     for (int j = 0; j < hours.Count; j++)
//                     {
//                         var currentHour = hours[j];

//                         // TODO ??? : Course not twice at the same time // secundary

//                         // créer la clé de l'emplacement // ((site, classroom), day, (startHour, endHour))
//                         var key = Tuple.Create(((currentClass.Item2, currentClass.Item1), currentDay, currentHour));

//                         if (!schedule.ContainsKey(key))
//                         {
//                             // TODO : check if the group is not already placed at the same time and remove groups already placed
//                             // TODO : Inter site travel time (1h)

//                             // on ajoute le cours au schedule
//                             schedule.Add(key, (currentCourse, courseGroupes[currentCourseIndex].Groupes.Select(g => g.Name).ToList()));

//                             // keep track of the current course
//                             var cg = courseGroupes[currentCourseIndex];

//                             // on retire du courseGroupes le cours qui vient d'être placé
//                             courseGroupes.RemoveAt(currentCourseIndex);

//                             // on continue avec le prochain cours
//                             if (BacktrackScheduleBis(daysOfWeek, hours, courseGroupes, classes, schedule, currentCourseIndex)) return true;

//                             // si on ne peut pas placer le prochain cours, on retire le cours actuel du schedule
//                             schedule.Remove(key);

//                             // on réajoute le cours retiré du courseGroupes
//                             courseGroupes.Add(cg);

//                             // on trie les courseGroupes restants par capacité
//                             courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();
//                         }
//                     }
//                 }
//             }
//             else // si la capacité de la classe n'est pas suffisante // on split le cours en plusieurs petits groupes
//             {
//                 // vérifier si on peut split le cours en plusieurs petits groupes
//                 if (courseGroupes[currentCourseIndex].Groupes.Count == 1) return false;

//                 // si la capacité de la classe n'est pas suffisante, on split le cours en plusieurs petits groupes
//                 var groupesToPlace = BacktrackClassroomsCoursGroupsBis(currentClass.Item3, courseGroupes[currentCourseIndex].Groupes);

//                 // on trouve un timeslot de libre pour la classe
//                 for (int d = 0; d < daysOfWeek.Count; d++)
//                 {
//                     var currentDay = daysOfWeek[d];

//                     // pour chaque timeslot de la jourée
//                     for (int j = 0; j < hours.Count; j++)
//                     {
//                         var currentHour = hours[j];

//                         // TODO ??? : Course not twice at the same time // secundary

//                         // créer la clé de l'emplacement // ((site, classroom), day, (startHour, endHour))
//                         var key = Tuple.Create(((currentClass.Item2, currentClass.Item1), currentDay, currentHour));

//                         if (!schedule.ContainsKey(key))
//                         {
//                             // TODO : check if the group is not already placed at the same time and remove groups already placed
//                             // TODO : Inter site travel time (1h)

//                             // on retire du courseGroupes le(s) groupes qui viennent d'être placés
//                             courseGroupes[currentCourseIndex].RemoveGroupes(groupesToPlace);

//                             // on trie les courseGroupes restants par capacité
//                             courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();

//                             // on place les groupes dans la classe
//                             schedule.Add(key, (currentCourse, groupesToPlace.Select(g => g.Name).ToList()));

//                             // on passe au prochain courseGroupe // on garde le currentCourseIndex car on prend toujours le premier cours (le plus gros)
//                             if (BacktrackScheduleBis(daysOfWeek, hours, courseGroupes, classes, schedule, currentCourseIndex)) return true;

//                             // si on ne peut pas placer le prochain coursGroupe, on le retire du schedule => free the timeslot
//                             schedule.Remove(key);

//                             // on réajoute les groupes retirés du courseGroupes
//                             courseGroupes[currentCourseIndex].Groupes.AddRange(groupesToPlace);

//                             // on trie les courseGroupes restants par capacité
//                             courseGroupes = courseGroupes.OrderByDescending(c => c.GetCapacity()).ToList();

//                             // on trie les classes restantes par capacité
//                             classes = classes.OrderByDescending(c => c.Item3).ToList();

//                             // on passe au suivant
//                             continue;
//                         }
//                     }
//                 }
//             }
//         }
//         return false;
//     }

//     List<Groupe> BacktrackClassroomsCoursGroupsBis(int capacity, List<Groupe> groupes)
//     {
//         // memoization
//         List<Groupe> bestCombinaison = new();
//         List<Groupe> currentCombinaison = new();
//         int bestCombinaisonCapacity = 0;

//         void Backtrack(int index, int currentSum)
//         {
//             // Vérification de la meilleure occupation atteinte
//             if (currentSum <= capacity && currentSum > bestCombinaisonCapacity)
//             {
//                 bestCombinaisonCapacity = currentSum;
//                 bestCombinaison = [.. currentCombinaison];
//             }

//             // Parcours des groupes restants
//             for (int i = index; i < groupes.Count; i++)
//             {
//                 if (currentSum + groupes[i].Capacity <= capacity)
//                 {
//                     // Ajouter le groupe dans la combinaison actuelle
//                     currentCombinaison.Add(groupes[i]);

//                     // Appel récursif pour explorer les autres combinaisons
//                     Backtrack(i + 1, currentSum + groupes[i].Capacity);

//                     // Retirer le groupe après l'exploration (backtracking)
//                     currentCombinaison.RemoveAt(currentCombinaison.Count - 1);
//                 }
//             }
//         }


//         Backtrack(0, 0);

//         return bestCombinaison;
//     }


//     bool TryPlaceCourseGroups(
//         List<CourseGroupes> coursesGroupes,
//         CourseGroupes courseGroupes,
//         List<string> days,
//         List<(int startHour, int endHour)> hours,
//         (string classroom, string site, int capacity) currentClass,
//         List<(string classroom, string site, int capacity)> classes,
//         string currentCourse,
//         Dictionary<
//         Tuple<
//             ((string site, string classroom) location,
//             string day,
//             (int startHour, int endHour) timeSlot)>,
//         (string course, List<string> groups)> schedule
//         )
//     {
//         //! Create specific function for this part
//         // on trouve un timeslot de libre pour la classe
//         for (int d = 0; d < days.Count; d++)
//         {
//             var currentDay = days[d];

//             // pour chaque timeslot de la jourée
//             for (int j = 0; j < hours.Count; j++)
//             {
//                 var currentHour = hours[j];

//                 // TODO ??? : Course not twice at the same time // secundary

//                 // créer la clé de l'emplacement // ((site, classroom), day, (startHour, endHour))
//                 var key = Tuple.Create(((currentClass.Item2, currentClass.Item1), currentDay, currentHour));

//                 if (!schedule.ContainsKey(key))
//                 {
//                     // TODO : check if the group is not already placed at the same time and remove groups already placed
//                     // TODO : Inter site travel time (1h)

//                     // on ajoute le cours au schedule
//                     schedule.Add(key, (currentCourse, groupesToPlace));

//                     // on continue avec le prochain cours
//                     if (BacktrackScheduleBis(days
//                     , hours, coursesGroupes, classes, schedule, 0)) return true;

//                     // si on ne peut pas placer le prochain cours, on retire le cours actuel du schedule
//                     schedule.Remove(key);
//                 }
//             }
//         }

//         // on réajoute le cours retiré du courseGroupes
//         coursesGroupes.Add(cg);

//         // on trie les courseGroupes restants par capacité
//         coursesGroupes = coursesGroupes.OrderByDescending(c => c.GetCapacity()).ToList();

//         return false;
//     }

// }