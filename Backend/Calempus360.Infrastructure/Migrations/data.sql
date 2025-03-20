BEGIN TRANSACTION;

-- Années académiques
DECLARE @AcademicYearId uniqueidentifier = NEWID();

INSERT INTO [AcademicYears] ([AcademicYearId], [AcademicYearCode], [DateStart], [DateEnd])
VALUES
(@AcademicYearId, '2024-2025', '2024-09-01', '2025-06-30');
-- Universités
DECLARE @UnivA uniqueidentifier = NEWID();
DECLARE @UnivB uniqueidentifier = NEWID();

INSERT INTO [Universities] ([UniversityId], [Name], [Code], [Phone], [Address])
VALUES 
(@UnivA, 'Université A', 'UNIVA', '0123456789', 'Adresse Université A'),
(@UnivB, 'Université B', 'UNIVB', '9876543210', 'Adresse Université B');
GO
-- Sites
DECLARE @SiteA1 uniqueidentifier = NEWID();
DECLARE @SiteA2 uniqueidentifier = NEWID();
DECLARE @SiteA3 uniqueidentifier = NEWID();
DECLARE @SiteB1 uniqueidentifier = NEWID();
DECLARE @SiteB2 uniqueidentifier = NEWID();
DECLARE @SiteB3 uniqueidentifier = NEWID();

DECLARE @UnivA uniqueidentifier = (SELECT UniversityId FROM Universities WHERE Code = 'UNIVA');
DECLARE @UnivB uniqueidentifier = (SELECT UniversityId FROM Universities WHERE Code = 'UNIVB');

INSERT INTO [Sites] ([SiteId], [UniversityId], [Code], [Name], [Phone], [Address])
VALUES 
(@SiteA1, @UnivA, 'SUA1', 'Site A1','123456781', 'Adresse Site A1'),
(@SiteA2, @UnivA, 'SUA2', 'Site A2','123456782', 'Adresse Site A2'),
(@SiteA3, @UnivA, 'SUA3', 'Site A3','123456783', 'Adresse Site A3'),
(@SiteB1, @UnivB, 'SUB1', 'Site B1','123456784', 'Adresse Site B1'),
(@SiteB2, @UnivB, 'SUB2', 'Site B2','123456785', 'Adresse Site B2'),
(@SiteB3, @UnivB, 'SUB3', 'Site B3','123456786', 'Adresse Site B3');

-- Plages horaires (2 plages par jour : matin et après-midi, du Lundi au Vendredi)
DECLARE @Schedules TABLE (ScheduleId uniqueidentifier, DayOfWeek int, HourStart TIME, HourEnd TIME);

INSERT INTO @Schedules (ScheduleId, DayOfWeek, HourStart, HourEnd)
VALUES 
    (NEWID(), 1, '08:00', '12:00'), -- Lundi matin
    (NEWID(), 1, '13:00', '17:00'), -- Lundi après-midi
    (NEWID(), 2, '08:00', '12:00'), -- Mardi matin
    (NEWID(), 2, '13:00', '17:00'), -- Mardi après-midi
    (NEWID(), 3, '08:00', '12:00'), -- Mercredi matin
    (NEWID(), 3, '13:00', '17:00'), -- Mercredi après-midi
    (NEWID(), 4, '08:00', '12:00'), -- Jeudi matin
    (NEWID(), 4, '13:00', '17:00'), -- Jeudi après-midi
    (NEWID(), 5, '08:00', '12:00'), -- Vendredi matin
    (NEWID(), 5, '13:00', '17:00'); -- Vendredi après-midi

INSERT INTO [CoursesSchedules] ([ScheduleId], [DayOfTheWeek], [HourStart], [HourEnd])
SELECT ScheduleId, DayOfWeek, HourStart, HourEnd FROM @Schedules;

-- Liaison Sites aux Plages Horaires (Chaque site a toutes les plages horaires)
INSERT INTO [SitesCoursesSchedules] ([SiteId], [ScheduleId])
SELECT SiteId, ScheduleId 
FROM (SELECT SiteId FROM Sites) AS AllSites
CROSS JOIN @Schedules;

-- Classrooms avec capacité variable entre 40 et 200 (auditoire inclus)
DECLARE @i int = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO [Classrooms] ([ClassroomId], [SiteId], [Code],  [Name], [Capacity])
    VALUES 
    (NEWID(), @SiteA1, CONCAT('Classe A1-', @i), CONCAT('CLA1-', @i), ROUND((RAND()*160)+40, 0)),
    (NEWID(), @SiteA2, CONCAT('Classe A2-', @i), CONCAT('CLA2-', @i), ROUND((RAND()*160)+40, 0)),
    (NEWID(), @SiteA3, CONCAT('Classe A3-', @i), CONCAT('CLA3-', @i), ROUND((RAND()*160)+40, 0)),
    (NEWID(), @SiteB1, CONCAT('Classe B1-', @i), CONCAT('CLB1-', @i), ROUND((RAND()*160)+40, 0)),
    (NEWID(), @SiteB2, CONCAT('Classe B2-', @i), CONCAT('CLB2-', @i), ROUND((RAND()*160)+40, 0)),
    (NEWID(), @SiteB3, CONCAT('Classe B3-', @i), CONCAT('CLB3-', @i), ROUND((RAND()*160)+40, 0));
    SET @i = @i + 1;
END

-- Options scolaires
DECLARE @OptionInfo uniqueidentifier = NEWID();
DECLARE @OptionGest uniqueidentifier = NEWID();
DECLARE @OptionMark uniqueidentifier = NEWID();
DECLARE @OptionRes uniqueidentifier = NEWID();
DECLARE @OptionDroit uniqueidentifier = NEWID();

INSERT INTO [Options] ([OptionId], [Name], [Code], [Description])
VALUES 
(@OptionInfo, 'Informatique', 'OPT_INFO', 'Option Informatique'),
(@OptionGest, 'Gestion', 'OPT_GEST', 'Option Gestion'),
(@OptionMark, 'Marketing', 'OPT_MARK', 'Option Marketing'),
(@OptionRes, 'Réseaux', 'OPT_RES', 'Option Réseaux'),
(@OptionDroit, 'Droit', 'OPT_DROIT', 'Option Droit');

-- Cours associés aux options
DECLARE @CourseInfo uniqueidentifier = NEWID();
DECLARE @CourseGest uniqueidentifier = NEWID();
DECLARE @CourseMark uniqueidentifier = NEWID();
DECLARE @CourseRes uniqueidentifier = NEWID();
DECLARE @CourseDroit uniqueidentifier = NEWID();

INSERT INTO [Courses] ([CourseId], [Name], [Code], [Description], [TotalHours], [WeeklyHours], [Semester], [Credits])
VALUES 
(@CourseInfo, 'Cours Informatique 1', 'INFO1', 'Description Info 1', 100, 5, 'S1', 5),
(@CourseGest, 'Cours Gestion 1', 'GEST1', 'Description Gestion 1', 100, 5, 'S1', 5),
(@CourseMark, 'Cours Marketing 1', 'MARK1', 'Description Marketing 1', 100, 5, 'S1', 5),
(@CourseRes, 'Cours Réseaux 1', 'RES1', 'Description Réseaux 1', 100, 5, 'S1', 5),
(@CourseDroit, 'Cours Droit 1', 'DROIT1', 'Description Droit 1', 100, 5, 'S1', 5);

-- Mapping OptionCourse
DECLARE @AcademicYearIdB uniqueidentifier = (SELECT TOP 1 AcademicYearId FROM AcademicYears);

INSERT INTO [OptionCourse] ([AcademicYearId], [CourseId], [OptionId], [OptionGrade])
VALUES
(@AcademicYearIdB, @CourseInfo, @OptionInfo, 1),
(@AcademicYearIdB, @CourseGest, @OptionGest, 1),
(@AcademicYearIdB, @CourseMark, @OptionMark, 1),
(@AcademicYearIdB, @CourseRes, @OptionRes, 1),
(@AcademicYearIdB, @CourseDroit, @OptionDroit, 1);

-- Equipements
DECLARE @EquipProj uniqueidentifier = NEWID();
DECLARE @EquipPC uniqueidentifier = NEWID();

INSERT INTO [EquipmentTypes] ([EquipmentTypeId], [Name], [Code], [Description])
VALUES 
(@EquipProj, 'Projecteur', 'EQUIP_PROJ', 'Projecteur vidéo'),
(@EquipPC, 'Ordinateur', 'EQUIP_PC', 'Ordinateur portable');


-- Équipements (chaque site a un équipement distinct si besoin)
-- On crée 2 projecteurs et 2 laptops (IDs différents), 
-- pour éviter qu'un même EquipmentId pointe vers deux sites.
DECLARE @ProjA1 uniqueidentifier = NEWID();
DECLARE @ProjB1 uniqueidentifier = NEWID();
DECLARE @LaptopA2 uniqueidentifier = NEWID();
DECLARE @LaptopB2 uniqueidentifier = NEWID();

-- Insert des équipements : 2 projecteurs, 2 laptops
INSERT INTO [Equipments] ([EquipmentId], [Name], [Code], [Brand], [Model], [Description], [EquipmentTypeId])
VALUES 
(@ProjA1,   'Projecteur Epson A1', 'PROJ_EPS_A1', 'Epson', 'X123', 'Projecteur HD pour Site A1', @EquipProj),
(@LaptopA2, 'Laptop Dell A2',     'LAP_DELL_A2', 'Dell',  'Inspiron', 'Laptop performant pour Site A2', @EquipPC),
(@ProjB1,   'Projecteur Epson B1', 'PROJ_EPS_B1', 'Epson', 'X124', 'Projecteur HD pour Site B1', @EquipProj),
(@LaptopB2, 'Laptop Dell B2',     'LAP_DELL_B2', 'Dell',  'Inspiron2', 'Laptop performant pour Site B2', @EquipPC);

-- Table de liaison entre Site / Equipement (un équipement unique pour un site)
INSERT INTO [UniversitiesSitesEquipments] ([UniversityId], [SiteId], [EquipmentId])
VALUES 
(@UnivA, @SiteA1, @ProjA1),    -- Projecteur Site A1
(@UnivA, @SiteA2, @LaptopA2),  -- Laptop    Site A2
(@UnivB, @SiteB1, @ProjB1),    -- Projecteur Site B1
(@UnivB, @SiteB2, @LaptopB2);  -- Laptop    Site B2

-- Association équipements à certains cours
INSERT INTO [CoursesEquipmentTypes] ([AcademicYearId], [CourseId], [EquipmentTypeId], [UniversityId])
VALUES
(@AcademicYearIdB, @CourseInfo, @EquipPC, @UnivA);

-- Groupes étudiants
INSERT INTO [StudentGroups] ([StudentGroupId], [Code], [OptionGrade],  [NumberOfStudents], [SiteId], [AcademicYearId], [OptionId])
VALUES 
(NEWID(), 'Groupe INFO 1', 1,  35, @SiteA1, @AcademicYearIdB, @OptionInfo),
(NEWID(), 'Groupe GEST 1', 1, 30, @SiteA2, @AcademicYearIdB, @OptionGest),
(NEWID(), 'Groupe MARK 1', 1, 32, @SiteB1,  @AcademicYearIdB, @OptionMark),
(NEWID(), 'Groupe RES 1', 1, 28, @SiteB2, @AcademicYearIdB, @OptionRes),
(NEWID(), 'Groupe DROIT 1', 1, 25, @SiteA3, @AcademicYearIdB, @OptionDroit);

IF @@ERROR <> 0
BEGIN
    ROLLBACK TRANSACTION;
    PRINT 'Erreur lors de l''insertion des données';
END
ELSE
BEGIN
    COMMIT TRANSACTION;
    PRINT 'Données insérées avec succès';
END