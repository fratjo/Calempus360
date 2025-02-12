IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [AcademicYears] (
    [AcademicYearId] int NOT NULL IDENTITY,
    [Year] nvarchar(max) NULL,
    [DateStart] datetime2 NOT NULL,
    [DateEnd] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_AcademicYears] PRIMARY KEY ([AcademicYearId])
);

CREATE TABLE [Courses] (
    [CourseId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [TotalHours] int NOT NULL,
    [WeeklyHours] int NOT NULL,
    [Semester] nvarchar(max) NOT NULL,
    [Credits] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_Courses] PRIMARY KEY ([CourseId])
);

CREATE TABLE [CoursesSchedules] (
    [ScheduleId] int NOT NULL IDENTITY,
    [DayOfTheWeek] int NOT NULL,
    [HourStart] datetime2 NOT NULL,
    [HourEnd] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_CoursesSchedules] PRIMARY KEY ([ScheduleId])
);

CREATE TABLE [EquipmentTypes] (
    [EquipmentTypeId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_EquipmentTypes] PRIMARY KEY ([EquipmentTypeId])
);

CREATE TABLE [Options] (
    [Option_Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_Options] PRIMARY KEY ([Option_Id])
);

CREATE TABLE [Universities] (
    [UniversityId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_Universities] PRIMARY KEY ([UniversityId])
);

CREATE TABLE [DaysWithoutCourse] (
    [DayWithoutCourseId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Date] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [AcademicYearId] int NOT NULL,
    CONSTRAINT [PK_DaysWithoutCourse] PRIMARY KEY ([DayWithoutCourseId]),
    CONSTRAINT [FK_DaysWithoutCourse_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE
);

CREATE TABLE [Equipments] (
    [EquipmentId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Brand] nvarchar(max) NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [EquipmentTypeId] int NOT NULL,
    CONSTRAINT [PK_Equipments] PRIMARY KEY ([EquipmentId]),
    CONSTRAINT [FK_Equipments_EquipmentTypes_EquipmentTypeId] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [EquipmentTypes] ([EquipmentTypeId]) ON DELETE CASCADE
);

CREATE TABLE [OptionCourse] (
    [AcademicYearId] int NOT NULL,
    [CourseId] int NOT NULL,
    [OptionId] int NOT NULL,
    [OptionGrade] int NOT NULL,
    CONSTRAINT [PK_OptionCourse] PRIMARY KEY ([CourseId], [OptionId], [AcademicYearId]),
    CONSTRAINT [FK_OptionCourse_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OptionCourse_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OptionCourse_Options_OptionId] FOREIGN KEY ([OptionId]) REFERENCES [Options] ([Option_Id]) ON DELETE CASCADE
);

CREATE TABLE [CoursesEquipmentTypes] (
    [AcademicYearId] int NOT NULL,
    [CourseId] int NOT NULL,
    [EquipmentTypeId] int NOT NULL,
    [UniversityId] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_CoursesEquipmentTypes] PRIMARY KEY ([EquipmentTypeId], [CourseId], [UniversityId], [AcademicYearId]),
    CONSTRAINT [FK_CoursesEquipmentTypes_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoursesEquipmentTypes_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoursesEquipmentTypes_EquipmentTypes_EquipmentTypeId] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [EquipmentTypes] ([EquipmentTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoursesEquipmentTypes_Universities_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [Universities] ([UniversityId]) ON DELETE CASCADE
);

CREATE TABLE [Sites] (
    [SiteId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UniversityId] int NOT NULL,
    [AcademicYearId] int NOT NULL,
    CONSTRAINT [PK_Sites] PRIMARY KEY ([SiteId]),
    CONSTRAINT [FK_Sites_Universities_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [Universities] ([UniversityId]) ON DELETE CASCADE
);

CREATE TABLE [Classrooms] (
    [ClassroomId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Capacity] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [SiteId] int NOT NULL,
    CONSTRAINT [PK_Classrooms] PRIMARY KEY ([ClassroomId]),
    CONSTRAINT [FK_Classrooms_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE CASCADE
);

CREATE TABLE [SitesAcademicYear] (
    [AcademicYearId] int NOT NULL,
    [SiteId] int NOT NULL,
    CONSTRAINT [PK_SitesAcademicYear] PRIMARY KEY ([SiteId], [AcademicYearId]),
    CONSTRAINT [FK_SitesAcademicYear_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SitesAcademicYear_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE CASCADE
);

CREATE TABLE [SitesCoursesSchedules] (
    [AcademicYearId] int NOT NULL,
    [SiteId] int NOT NULL,
    [ScheduleId] int NOT NULL,
    CONSTRAINT [PK_SitesCoursesSchedules] PRIMARY KEY ([SiteId], [ScheduleId], [AcademicYearId]),
    CONSTRAINT [FK_SitesCoursesSchedules_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SitesCoursesSchedules_CoursesSchedules_ScheduleId] FOREIGN KEY ([ScheduleId]) REFERENCES [CoursesSchedules] ([ScheduleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SitesCoursesSchedules_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE CASCADE
);

CREATE TABLE [StudentGroups] (
    [GroupId] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [NumberOfStudents] int NOT NULL,
    [OptionGrade] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (getdate()),
    [AcademicYearId] int NOT NULL,
    [SiteId] int NOT NULL,
    [OptionId] int NOT NULL,
    CONSTRAINT [PK_StudentGroups] PRIMARY KEY ([GroupId]),
    CONSTRAINT [FK_StudentGroups_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentGroups_Options_OptionId] FOREIGN KEY ([OptionId]) REFERENCES [Options] ([Option_Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentGroups_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE CASCADE
);

CREATE TABLE [UniversitiesSitesEquipments] (
    [AcademicYearId] int NOT NULL,
    [EquipmentId] int NOT NULL,
    [SiteId] int NOT NULL,
    [UniversityId] int NOT NULL,
    CONSTRAINT [PK_UniversitiesSitesEquipments] PRIMARY KEY ([EquipmentId], [AcademicYearId]),
    CONSTRAINT [FK_UniversitiesSitesEquipments_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UniversitiesSitesEquipments_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([EquipmentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UniversitiesSitesEquipments_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UniversitiesSitesEquipments_Universities_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [Universities] ([UniversityId]) ON DELETE CASCADE
);

CREATE TABLE [ClassroomsAcademicYear] (
    [AcademicYearId] int NOT NULL,
    [ClassroomId] int NOT NULL,
    CONSTRAINT [PK_ClassroomsAcademicYear] PRIMARY KEY ([ClassroomId], [AcademicYearId]),
    CONSTRAINT [FK_ClassroomsAcademicYear_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClassroomsAcademicYear_Classrooms_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [Classrooms] ([ClassroomId]) ON DELETE CASCADE
);

CREATE TABLE [ClassroomsEquipments] (
    [AcademicYearId] int NOT NULL,
    [EquipmentId] int NOT NULL,
    [ClassroomId] int NOT NULL,
    CONSTRAINT [PK_ClassroomsEquipments] PRIMARY KEY ([EquipmentId], [ClassroomId], [AcademicYearId]),
    CONSTRAINT [FK_ClassroomsEquipments_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClassroomsEquipments_Classrooms_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Classrooms] ([ClassroomId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClassroomsEquipments_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([EquipmentId]) ON DELETE CASCADE
);

CREATE TABLE [Sessions] (
    [SessionId] int NOT NULL IDENTITY,
    [DatetimeStart] datetime2 NOT NULL,
    [DatetimeEnd] datetime2 NOT NULL,
    [ClassroomId] int NOT NULL,
    [CourseId] int NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY ([SessionId]),
    CONSTRAINT [FK_Sessions_Classrooms_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [Classrooms] ([ClassroomId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Sessions_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]) ON DELETE CASCADE
);

CREATE TABLE [EquipmentSessions] (
    [EquipmentId] int NOT NULL,
    [SessionId] int NOT NULL,
    CONSTRAINT [PK_EquipmentSessions] PRIMARY KEY ([EquipmentId], [SessionId]),
    CONSTRAINT [FK_EquipmentSessions_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([EquipmentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EquipmentSessions_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([SessionId]) ON DELETE CASCADE
);

CREATE TABLE [StudentGroupSessions] (
    [StudentGroupId] int NOT NULL,
    [SessionId] int NOT NULL,
    CONSTRAINT [PK_StudentGroupSessions] PRIMARY KEY ([StudentGroupId], [SessionId]),
    CONSTRAINT [FK_StudentGroupSessions_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([SessionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentGroupSessions_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [StudentGroups] ([GroupId]) ON DELETE CASCADE
);

CREATE INDEX [IX_Classrooms_SiteId] ON [Classrooms] ([SiteId]);

CREATE INDEX [IX_ClassroomsAcademicYear_AcademicYearId] ON [ClassroomsAcademicYear] ([AcademicYearId]);

CREATE INDEX [IX_ClassroomsEquipments_AcademicYearId] ON [ClassroomsEquipments] ([AcademicYearId]);

CREATE UNIQUE INDEX [IX_ClassroomsEquipments_EquipmentId] ON [ClassroomsEquipments] ([EquipmentId]);

CREATE INDEX [IX_CoursesEquipmentTypes_AcademicYearId] ON [CoursesEquipmentTypes] ([AcademicYearId]);

CREATE INDEX [IX_CoursesEquipmentTypes_CourseId] ON [CoursesEquipmentTypes] ([CourseId]);

CREATE INDEX [IX_CoursesEquipmentTypes_UniversityId] ON [CoursesEquipmentTypes] ([UniversityId]);

CREATE INDEX [IX_DaysWithoutCourse_AcademicYearId] ON [DaysWithoutCourse] ([AcademicYearId]);

CREATE INDEX [IX_Equipments_EquipmentTypeId] ON [Equipments] ([EquipmentTypeId]);

CREATE INDEX [IX_EquipmentSessions_SessionId] ON [EquipmentSessions] ([SessionId]);

CREATE INDEX [IX_OptionCourse_AcademicYearId] ON [OptionCourse] ([AcademicYearId]);

CREATE INDEX [IX_OptionCourse_OptionId] ON [OptionCourse] ([OptionId]);

CREATE INDEX [IX_Sessions_ClassroomId] ON [Sessions] ([ClassroomId]);

CREATE INDEX [IX_Sessions_CourseId] ON [Sessions] ([CourseId]);

CREATE INDEX [IX_Sites_UniversityId] ON [Sites] ([UniversityId]);

CREATE INDEX [IX_SitesAcademicYear_AcademicYearId] ON [SitesAcademicYear] ([AcademicYearId]);

CREATE INDEX [IX_SitesCoursesSchedules_AcademicYearId] ON [SitesCoursesSchedules] ([AcademicYearId]);

CREATE INDEX [IX_SitesCoursesSchedules_ScheduleId] ON [SitesCoursesSchedules] ([ScheduleId]);

CREATE INDEX [IX_StudentGroups_AcademicYearId] ON [StudentGroups] ([AcademicYearId]);

CREATE INDEX [IX_StudentGroups_OptionId] ON [StudentGroups] ([OptionId]);

CREATE INDEX [IX_StudentGroups_SiteId] ON [StudentGroups] ([SiteId]);

CREATE INDEX [IX_StudentGroupSessions_SessionId] ON [StudentGroupSessions] ([SessionId]);

CREATE INDEX [IX_UniversitiesSitesEquipments_AcademicYearId] ON [UniversitiesSitesEquipments] ([AcademicYearId]);

CREATE UNIQUE INDEX [IX_UniversitiesSitesEquipments_EquipmentId] ON [UniversitiesSitesEquipments] ([EquipmentId]);

CREATE INDEX [IX_UniversitiesSitesEquipments_SiteId] ON [UniversitiesSitesEquipments] ([SiteId]);

CREATE INDEX [IX_UniversitiesSitesEquipments_UniversityId] ON [UniversitiesSitesEquipments] ([UniversityId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250212214757_InitialCreate', N'9.0.2');

COMMIT;
GO

