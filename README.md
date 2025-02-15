# Calempus360

---

# Cahier des charges

1. **Introduction**
    - **Objectif du document** : Décrire pourquoi le document est créé et ce qu'il couvre.
      Ce document, en tant que cahier des charges d’un projet informatique de développement, a pour objectif principal de fournir une base claire et commune aux différentes parties prenantes. Il détaille à la fois les aspects métiers, les contraintes techniques, et les exigences spécifiques du projet. Ce document sert également de guide de référence tout au long du cycle de vie du projet, garantissant ainsi une compréhension partagée et une orientation cohérente vers les objectifs définis.
    - **Contexte** : Présentation du problème à résoudre ou des opportunités à exploiter.
      La gestion des horaires dans un contexte universitaire multi-site représente un défi majeur en raison des contraintes liées aux déplacements entre sites, à la disponibilité des infrastructures, et à la coordination des groupes d’étudiants. Ce projet vise à répondre à ces problématiques en développant un système efficace, automatisé et flexible permettant de simplifier la gestion des emplois du temps tout en respectant les spécificités propres à chaque université.
    - **Portée du projet** : Définir clairement les limites du projet.
      Le projet se concentre exclusivement sur la gestion des horaires académiques pour les universités et leurs infrastructures associées. Il couvrira :
        - La génération automatique et optimisée des emplois du temps.
        - La modification manuelle des horaires par les administrateurs.
        - La visualisation des horaires pour les groupes d’étudiants.
          Le système ne traitera pas d’autres aspects de la gestion universitaire, comme l’inscription des étudiants ou la gestion des enseignants.
    - **Parties prenantes** : Identifier les parties prenantes principales et secondaires.
        - **Client principal** :
            - **Monsieur V. Fievez**, représentant officiel des universités impliquées.Il assure le rôle de sponsor et principal décisionnaire pour les priorités et la validation des livrables.
        - **Équipe de développement** :
            - Composée d’étudiants en charge de la conception, du développement, et de la mise en œuvre du projet.Cette équipe sera également responsable de la présentation des résultats et de la documentation technique associée.
2. **Description générale du système**
    - **Vision produit** : Résumé de haut niveau des objectifs du produit ou système.
        1. Le système doit permettre la gestion des horaires de cours dans une université composées d’un ou plusieurs sites. Le système doit atteindre quelques objectifs :
            - Génération optimale et automatique des horaires : Il faut pouvoir optimiser l’attribution des cours en fonction des sites, des groupes d’étudiants et du matériel requis, tout en sachant que le déplacement inter-sites prend 1 heures.
            - Interface utilisateurs : Il doit avoir 2 interfaces utilisateurs, une interface pour l’administrateur et une autre pour le groupe d’étudiants.
            - Gestion du matériel : Il faut pouvoir gérer le matériel fixe ou mobile pour s’assurer que chaque cours dispose du matériel nécessaire.
            - Gestion des classes : Il faut pouvoir gérer les classes d’un site en respectant les contraintes de place maximal disponible dans une classe.
            - Gestions des cours : Il faut pouvoir gérer les cours en s’assurant de respecter les contraintes de durée des cours.
            - Gestion des groupes : Il faut pouvoir gérer les groupes en respectant les contraintes de limite de taille d’un groupe ainsi que leur heure de cours par semaine.
            - Gestion de plusieurs universités : Il faut pouvoir gérer plusieurs universités ayant chacune leur propres sites et étudiants.
    - **Contexte opérationnel** : Décrire comment le système s'intègre dans l'environnement existant.
      Dans un premier temps le système fonctionnera en solitaire.
      Il pourra par après aller puiser ses données dans les bases de données de l’université afin de récolter les données des sites, classes, groupes, équipements, options, cours.
    - **Contraintes générales** : Les contraintes techniques, réglementaires, ou organisationnelles.
        - Contraintes techniques : L’interface utilisateurs doit passer par une web API pour communiquer avec la base de données. Le back-end est une web API obligatoire composée d’une architecture qui contient au minimum des services, repositories et des models. De plus, le back-end doit être composé de deux controllers, un pour l’administrateur et un pour le groupe d’étudiants.
        - Contraintes réglementaires : Le calendrier académique doit respecter la structure scolaire standard avec des semestres ou trimestres et la mise en place de jours féries légaux ou encore de jours de fermetures spéciaux spécifiques à l’université.
3. **Exigences des parties prenantes**
    - **Objectifs des parties prenantes** : Prioriser leurs besoins et attentes.
      Les besoins et attentes des parties prenantes sont priorisés pour garantir une progression cohérente et un alignement avec les objectifs du projet. Les principaux objectifs identifiés sont les suivants :
        1. **Démonstration du MVP (Minimum Viable Product)** :
            - Le client, **Monsieur V. Fievez**, souhaite disposer d’une version démontrable du produit minimal viable (MVP) d’ici début avril. Cette version devra inclure les fonctionnalités essentielles, permettant ainsi de valider la faisabilité et de collecter des retours pour d’éventuelles améliorations.
    - **Critères de succès** : Définir comment la réussite sera mesurée.
      La réussite du projet sera évaluée à travers une série de tests et de validations basés sur les exigences définies. Les principaux critères de succès incluent :
        1. **Visualisation des horaires** :
            - Vérification de l’affichage des emplois du temps dans différents formats :
                - **Point de vue étudiant** : Consultation des cours d’un groupe donné sous différents modes (journalier, hebdomadaire, mensuel).
                - **Point de vue des cours** : Suivi des plages horaires associées à un cours particulier.
        2. **Génération automatique des horaires** :
            - Validation de la capacité du système à produire automatiquement un emploi du temps optimisé pour une année scolaire, tout en respectant les contraintes liées aux déplacements, aux disponibilités des salles et des équipements.
        3. **Modification manuelle des horaires** :
            - Test de l’interface dédiée à l’administration permettant de modifier un ou plusieurs événements (par exemple, déplacer une session de cours) avec une prise en compte immédiate des changements.
4. **Règles métier**
    - **Contraintes métier**
        ### **1. Année académique**
        1. L’année académique commence et se termine à des dates données.
        2. La période définie entre la date de début et la date de fin constitue la période des cours.
        3. L’année académique inclut un calendrier spécifique comportant :
            - Journées sans cours (vacances, grèves, journées pédagogiques, fermetures exceptionnelles, etc.).
            - Plages horaires actives pour chaque site.
        ### **2. Université**
        1. Une université peut gérer plusieurs sites.
        2. Les universités fonctionnent de manière indépendante les unes des autres.
        ### **3. Site**
        1. Un site peut comporter une ou plusieurs classes.
        2. Un site appartient exclusivement à une université.
        3. Un site peut être ajouté ou supprimé (de manière hard ou soft) d’une année scolaire à l’autre.
        4. Chaque site dispose de plages horaires spécifiques, qui peuvent inclure des pauses distinctes (exemple : pause déjeuner divisant la journée en deux plages).
        5. Les sites peuvent exiger des équipements spécifiques pour certains cours ou activités.
        ### **4. Classe**
        1. Une classe appartient exclusivement à un site donné.
        2. Une classe peut être ajoutée ou supprimée (de manière hard ou soft) d’une année scolaire à l’autre.
        3. Une classe peut être équipée de zéro ou plusieurs équipements, spécifiques à l’année académique.
        4. Une classe peut accueillir :
            - Zéro ou plusieurs sessions de cours.
            - Zéro ou plusieurs groupes d’étudiants.
            - Zéro ou plusieurs équipements nécessaires.
        ### **5. Groupe d’étudiants**
        1. Chaque groupe d’étudiants a un site principal (site de référence) pour une année scolaire donnée.
        2. Un groupe suit une seule option par année académique.
        3. La taille d’un groupe est définie, avec des limites minimum et maximum (exemple : 20 à 40 étudiants).
        ### **6. Option**
        1. Une option est définie par un nombre d’années de formation :
            - Par exemple : 3 ans pour un bachelier, 1 à 2 ans pour un master.
        2. Une option est associée à un ensemble de cours spécifiques.
        3. Chaque année d’une option (appelée "Grade d’Option") peut être suivie par zéro ou plusieurs groupes d’étudiants en fonction de l’année scolaire.
        ### **7. Cours**
        1. Un cours peut être associé à une ou plusieurs options, en fonction de l’année académique.
        2. Un cours est unique pour une option donnée et une année spécifique de cette option.
        3. Un cours peut nécessiter :
            - Un ou plusieurs types d’équipements.
            - Une quantité donnée de chaque types d’équipement.
        4. Un cours peut être enseigné dans :
            - Zéro ou plusieurs classes.
            - Zéro ou plusieurs groupes d’étudiants.
        5. Un cours est associé à zéro ou plusieurs sessions de cours, comprenant un ensemble précis de ressources.
        ### **8. Session de cours**
        1. Une session de cours est la combinaison de :
            - Une classe.
            - Un cours.
            - Un ou plusieurs groupes d’étudiants.
            - Un ou plusieurs équipements nécessaires.
        2. Les sessions de cours doivent respecter les contraintes horaires et les disponibilités des sites, classes, et équipements.
        ### **9. Équipements**
        1. Chaque équipement appartient à un type spécifique (par exemple : projecteurs, ordinateurs, écrans tactiles).
        2. Un type d’équipement peut inclure plusieurs équipements similaires mais de marques différentes.
        3. Les équipements peuvent être :
            - Fixes (attribués à une seule classe).
            - Mobiles (utilisables dans plusieurs classes ou sites).
    - Diagramme E-A
5. **Exigences fonctionnelles**

    - Liste structurée des fonctionnalités que le système doit fournir.
        - FR-001 : Le système doit permettre la création automatique et la gestion manuelle des horaires.
        - FR-002 : Le système doit permettre la gestion des groupes.
        - FR-003 : Le système doit permettre la gestion des cours.
        - FR-004 : Le système doit permettre la gestion des sites.
        - FR-005 : Le système doit permettre de gérer des classes.
        - FR-006 : Le système doit permettre la gestion des équipements audio-visuel.
    - Décrire les cas d’utilisation ou les scénarios utilisateur pertinents.
        - Use Case
            - Consulter les cours :
              Ce cas d’utilisation permet à un groupe d’étudiants de consulter leurs cours c-à-d les détails de celui-ci. En sélectionnant un cours spécifique, le système affiche des informations détaillées comme le nom du cours, sa description, la date et l’heure, le site et la classe où les prochaines sessions du cours se déroule.
            - Consulter les horaires :
              Ce cas d’utilisation permet à un groupe d’étudiants de consulter son horaire via le calendrier. L’horaire affiche les cours sur une semaine ou sur une période spécifiques. Quelques informations sur le cours peuvent être directement consulté via la vue du calendrier tels que le site, la classe ou encore si un changement de site est requis pour le groupe d’étudiant qui consulte son calendrier.
            - Gestion des horaires : Ce cas d’utilisation permet à l’administrateur de gérer les horaires en ajoutant, modifiant ou supprimant des cours. Il peut également générer automatiquement les cours de manière optimisé en tenant comptes des différents contraintes données. L’administrateur peut aussi modifier l’horaire sachant que si un conflit d’horaire est présent, il sera signalé à l’administrateur et celui-ci pourra décider ou non de maintenir sa modification d’horaires.
            - Gestion des groupes : Ce cas d’utilisation permet à l’administrateur d’ajouter, modifier ou supprimer un groupe d’étudiants. Si la modification du groupe impact l’horaire(changement de taille classe trop petite?), l’administrateur sera planifié.
            - Gestion des cours : Ce cas d’utilisation permet à l’administrateur de gérer les cours en ajoutant, modifiant ou supprimant ceci. Certains cours nécessitent des équipements spécifiques, donc lorsqu’une modification est effectuée, le système vérifie l’impact sur les emplois du temps et signale les conflits tels qu’un manque de matériel.
            - Gestion des sites : Ce cas d’utilisation permet à l’administrateur d’ajouter, modifier, ou supprimer un site. Lors d’une modification, si une classe est supprimé ou si sa capacité est modifié, le système signalera ceci à l’administrateur qui choisira d’agir ou pas.
            - Gestion des équipement : Ce cas d’utilisation permet à l’administrateur de gérer les équipements audiovisuels en ajoutant du nouveaux matériels, en modifiant leur caractéristiques ou en les supprimant. Lorsqu’une modification est effectuée, le système vérifie l’impact sur les horaires et signale les conflits, comme un équipement requis déjà attribué pour une autre classe.
    - Associer chaque fonctionnalité aux objectifs des parties prenantes.
        - Objectif 1 : La génération automatique et optimales des horaires.
            - FR-001
        - Objectif 2 : UI pour administrateur et groupe d’étudiant.
        - Objectif 3 : Gestion du matériel et des classes.
            - FR-005 + FR-006
        - Objectif 4 : Gestion des groupes
            - FR-002
        - Objectif 5 : Gestion multi-universitaires

6. **Exigences non fonctionnelles**

    ### **1. Performance**

    - Le système devra offrir une **vitesse d’exécution élevée**, permettant une navigation fluide, même en cas de forte charge.
    - Les actions courantes, telles que la consultation, la modification ou la génération automatique des horaires, devront être réalisées en moins de **3 secondes** dans des conditions normales d’utilisation.
    - Le système devra être capable de gérer efficacement **plusieurs universités simultanément**, avec un nombre important de groupes, classes et sessions de cours.

    ### **2. Utilisabilité**

    - L’application devra offrir une **interface utilisateur intuitive et moderne**, permettant une prise en main rapide pour les administrateurs et les étudiants.
    - Les actions importantes (par exemple, modification d’un horaire, visualisation d’un emploi du temps) devront être accessibles en **trois clics maximum**.

    ### **3. Flexibilité**

    - Le système devra permettre une **configuration adaptable** aux spécificités de chaque université, site, ou année académique, notamment :
        - Les plages horaires spécifiques.
        - Les besoins en équipements ou ressources.
    - Il devra être facile d’ajouter ou de modifier des règles métiers (exemple : nouvelles contraintes pour les déplacements, les équipements, ou les cours).

    ### **4. Sécurité**

    - Toutes les opérations (consultation, ajout, modification, suppression des données) devront impérativement être réalisées via l’**API REST**.
    - Aucune interaction directe avec la base de données ne sera autorisée depuis les interfaces utilisateur.
    - Les échanges de données entre le client et l’API devront être sécurisés par le protocole **HTTPS**.
    - Les données critiques (horaires, informations sur les cours, etc.) devront être protégées contre les accès non autorisés via des mécanismes de contrôle d’accès et de validation.
    - Une gestion des erreurs robuste devra être mise en place pour éviter les failles potentielles dues à des entrées malveillantes.

    ### **5. Maintenabilité**

    - Le code devra être structuré en suivant des principes de **clean code** et des standards de l’industrie pour garantir la lisibilité et la facilité de maintenance.
    - Une architecture modulaire (avec séparation des couches services, repositories, modèles, etc.) devra être adoptée pour permettre des évolutions futures sans impact majeur sur l’existant.

    ### **6. Compatibilité**

    - Le système devra être compatible avec les navigateurs modernes (Chrome, Firefox, Edge, Safari) dans leurs deux dernières versions.
    - Les interfaces devront être responsives, offrant une expérience utilisateur cohérente sur ordinateurs, tablettes et smartphones.

7. **Contraintes et interfaces**

    - **Contraintes matérielles et logicielles** : Technologies spécifiques à utiliser.
        ### **Technologies spécifiques à utiliser**
        - Diagramme de composant
        1. **Frontend** :
            - Framework : **Angular**.
            - Responsiveness : Compatible avec les appareils de bureau et mobiles.
            - Bibliothèque UI : Utilisation d’une bibliothèque moderne comme **Tailwind CSS + DaisyUI** pour assurer une interface cohérente et esthétique.
        2. **Backend** :
            - Langage : **C# avec ASP.NET Core**.
            - ORM : Utilisation de **Entity Framework Core** ou **Dapper** (selon les besoins de performance).
            - API : Développement d’un **API REST** permettant une communication standardisée entre le frontend et le backend.
        3. **Base de données** :
            - SGBD : **SQL Server**.
            - Structure : Respect des principes de normalisation pour garantir une intégrité des données et des performances optimales.
            - Diagramme E-R,
        4. **Environnement de développement** :
            - Gestionnaire de versions : **Git** (avec utilisation des branches et des pull requests pour gérer les évolutions).
            - IDE recommandés :
                - **Visual Studio, Visual Studio Code ou Rider** pour le backend.
                - **Visual Studio Code ou WebStorm** pour le frontend.
    - **Interfaces utilisateur** : Maquettes ou descriptions de l’interaction utilisateur.
        - Diagramme de classe UML
        - **Administrateurs** :
            - **Tableau de bord principal** :
                - Vue globale des universités, sites, classes, et équipements.
                - Accès rapide à la génération, modification, et gestion des horaires.
            - **Éditeur d’horaires** :
                - Interface interactive permettant de déplacer, ajouter, ou modifier des sessions de cours.
                - Visualisation des conflits ou contraintes non respectées en temps réel (avec des alertes visuelles).
            - **Gestion des ressources** :
                - Accès à la gestion des équipements, salles, et groupes d’étudiants.
        - **Étudiants** :
            - **Consultation des horaires** :
                - Vue simplifiée des cours, avec des filtres par jour, semaine, ou mois.
        - **Conception visuelle** :
            - Proposer des maquettes respectant les principes d’ergonomie et d’accessibilité (WCAG 2.1).
    - **Interfaces externes** : Dépendances avec d'autres systèmes ou applications.
      **Systèmes de gestion universitaire existants** (optionnel) :
        - Possibilité d’intégration avec des logiciels tiers pour récupérer des données sur les groupes d’étudiants, les options ou les cours, si les universités disposent déjà d’un tel système.

8. **Traçabilité des exigences**
    - Mapping Requirements vers Agile/Scrum (Epic, Story, Task)
9. **Analyse des risques**

    ### **Identification des risques**

    1. **Risques techniques**
        - **Complexité de l’algorithme de génération des horaires** :
            - Le besoin de combiner des approches de **backtracking** et **gloutonnes** peut engendrer des temps d’exécution élevés, notamment pour des universités avec un grand nombre de groupes, cours, et sites.
            - La gestion des contraintes multiples (disponibilités, déplacements, équipements, etc.) peut mener à des scénarios où la solution optimale est difficile à trouver ou inexistante.
        - **Problèmes de performance** :
            - Temps de réponse inadéquat lors de la génération des horaires pour des cas complexes.
            - Impact sur l’expérience utilisateur si les requêtes prennent trop de temps ou échouent.
    2. **Risques organisationnels**
        - **Manque de clarté dans les exigences métiers** :
            - Difficulté à collecter toutes les règles et exceptions liées aux horaires, spécifiques à chaque université ou site.
        - **Collaboration et gestion de projet** :
            - Problèmes de coordination entre les membres de l’équipe de développement, surtout si les délais sont courts.
            - Non-respect des échéances (exemple : démonstration du MVP en avril).

10. **Hypothèses et dépendances**

    ### **Hypothèses**

    Les hypothèses sur lesquelles repose le projet sont les suivantes :

    1. **Données d’entrée** :
        - Les règles métiers spécifiques à chaque université ou site seront clarifiées et validées avant la phase de développement.
    2. **Accès et infrastructure** :
        - Les environnements de développement, de test et de production seront accessibles à l’équipe de développement sans interruption majeure.
        - Les ressources matérielles et logicielles nécessaires (serveurs, bases de données, outils de gestion) seront disponibles et configurées correctement.
    3. **Collaboration et feedback** :
        - Les parties prenantes fourniront des retours rapides et clairs sur les maquettes, les prototypes (MVP), et les livrables intermédiaires pour permettre des ajustements efficaces.
    4. **Algorithme de génération** :
        - Une solution combinant des approches gloutonnes et de backtracking pourra répondre aux contraintes de performance et produire des résultats satisfaisants pour la majorité des cas. Les contraintes métier resteront dans des limites réalistes, évitant des scénarios où aucune solution valable ne pourrait être trouvée.

    ### **Dépendances**

    Les dépendances critiques qui pourraient affecter le succès du projet sont les suivantes :

    1. **Disponibilité des données** :
        - Toute erreur ou retard dans la fourniture des données d’entrée pourrait entraîner des retards dans la phase de développement ou compromettre la qualité des résultats.
    2. **Outils et technologies** :
        - La réussite du projet dépend de la compatibilité et de la stabilité des technologies choisies (Angular, ASP.NET Core, SQL Server, etc.).
        - Les mises à jour ou modifications imprévues des outils de développement ou des bibliothèques utilisées pourraient engendrer des ajustements imprévus.
    3. **Collaboration entre les membres de l’équipe** :
        - Une dépendance à une collaboration efficace entre les développeurs pour éviter des conflits liés au code (par exemple, sur Git) ou des retards dans les tâches critiques.
    4. **Participation des parties prenantes** :
        - Le succès du projet repose sur l’implication active des parties prenantes (administrateurs, étudiants, etc.) pour valider les fonctionnalités clés et fournir des retours d’amélioration.
    5. **Limites organisationnelles** :
        - Des imprévus, tels que des absences prolongées au sein de l’équipe de développement ou des interruptions de communication avec le client, pourraient compromettre le respect des échéances.

11. **Critères d’acceptation et validation**

    **Critères d’acceptation**

    Les fonctionnalités suivantes doivent être testées et validées pour que le système soit accepté :

    1. **Visualisation des horaires** :
        - Les étudiants doivent pouvoir afficher leurs emplois du temps en différents formats : journalier, hebdomadaire, et mensuel.
        - Les administrateurs doivent pouvoir visualiser les horaires par groupe, par cours, ou par site.
    2. **Génération automatique des horaires** :
        - Le système doit être capable de générer un emploi du temps complet et optimisé pour une année académique donnée en respectant :
            - Les contraintes de déplacement entre sites.
            - Les disponibilités des classes et des équipements.
            - Les plages horaires spécifiques à chaque site.
        - Les conflits non résolus doivent être signalés clairement.
    3. **Modification manuelle des horaires** :
        - L’interface dédiée aux administrateurs doit permettre de :
            - Déplacer un cours existant vers une autre plage horaire ou un autre site.
            - Ajouter ou supprimer une session de cours.
        - Les modifications doivent être immédiatement reflétées dans la vue des horaires.
    4. **Gestion des ressources** :
        - Les administrateurs doivent pouvoir ajouter, modifier ou désactiver des sites, classes, groupes d’étudiants, cours, ou équipements.
        - Les modifications doivent respecter les contraintes définies (par exemple, un site ne peut être associé qu’à une seule université).
    5. **Sécurité et intégrité des données** :
        - Toutes les opérations (consultation, ajout, modification, suppression) doivent être effectuées exclusivement via l’API REST.
        - Les données ne doivent pas être accessibles directement depuis le frontend.
    6. **Performance** :
        - Le temps de réponse pour les actions critiques (génération d’horaires, modification d’une session) ne doit pas excéder **3 secondes** dans des conditions normales d’utilisation.

    ### **Validation**

    Les tests suivants seront effectués pour valider le système :

    1. **Tests fonctionnels** :
        - Validation des fonctionnalités clés listées ci-dessus, selon des scénarios réalistes et couvrant les cas nominaux et extrêmes.
        - Utilisation de cas de test préalablement définis pour garantir une couverture exhaustive.
    2. **Tests d’interface utilisateur** :
        - Validation de l’ergonomie et de l’intuitivité de l’interface pour les utilisateurs finaux.
        - Tests d’accessibilité pour garantir une expérience utilisateur cohérente sur tous les appareils (ordinateurs, tablettes, smartphones).
    3. **Recette utilisateur (User Acceptance Testing)** :
        - Les administrateurs et étudiants testeront le système dans des conditions réelles pour valider son adéquation avec leurs besoins.
    4. **Démonstration du MVP** :
        - Présentation des fonctionnalités essentielles du système au client et validation de leur conformité avec les attentes définies.

12. **Glossaire**
    - Définitions des termes spécifiques, acronymes ou jargons utilisés dans le projet.

---
