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
    [AcademicYearId] uniqueidentifier NOT NULL,
    [AcademicYearCode] nvarchar(max) NOT NULL,
    [DateStart] DATE NOT NULL,
    [DateEnd] DATE NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_AcademicYears] PRIMARY KEY ([AcademicYearId])
);

CREATE TABLE [Courses] (
    [CourseId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [TotalHours] int NOT NULL,
    [WeeklyHours] int NOT NULL,
    [Semester] nvarchar(max) NOT NULL,
    [Credits] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_Courses] PRIMARY KEY ([CourseId])
);

CREATE TABLE [CoursesSchedules] (
    [ScheduleId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [DayOfTheWeek] int NOT NULL,
    [HourStart] TIME NOT NULL,
    [HourEnd] TIME NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_CoursesSchedules] PRIMARY KEY ([ScheduleId])
);

CREATE TABLE [EquipmentTypes] (
    [EquipmentTypeId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(450) NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_EquipmentTypes] PRIMARY KEY ([EquipmentTypeId])
);

CREATE TABLE [Options] (
    [OptionId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_Options] PRIMARY KEY ([OptionId])
);

CREATE TABLE [Universities] (
    [UniversityId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(450) NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    [Phone] nvarchar(450) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_Universities] PRIMARY KEY ([UniversityId])
);

CREATE TABLE [DaysWithoutCourse] (
    [DayWithoutCourseId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(max) NOT NULL,
    [Date] DATE NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [AcademicYearId] uniqueidentifier NULL,
    CONSTRAINT [PK_DaysWithoutCourse] PRIMARY KEY ([DayWithoutCourseId]),
    CONSTRAINT [FK_DaysWithoutCourse_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE
);

CREATE TABLE [Equipments] (
    [EquipmentId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Brand] nvarchar(max) NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [EquipmentTypeId] uniqueidentifier NULL,
    CONSTRAINT [PK_Equipments] PRIMARY KEY ([EquipmentId]),
    CONSTRAINT [FK_Equipments_EquipmentTypes_EquipmentTypeId] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [EquipmentTypes] ([EquipmentTypeId]) ON DELETE SET NULL
);

CREATE TABLE [OptionCourse] (
    [AcademicYearId] uniqueidentifier NOT NULL,
    [CourseId] uniqueidentifier NOT NULL,
    [OptionId] uniqueidentifier NOT NULL,
    [OptionGrade] int NOT NULL,
    CONSTRAINT [PK_OptionCourse] PRIMARY KEY ([CourseId], [OptionId], [AcademicYearId]),
    CONSTRAINT [FK_OptionCourse_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OptionCourse_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OptionCourse_Options_OptionId] FOREIGN KEY ([OptionId]) REFERENCES [Options] ([OptionId]) ON DELETE CASCADE
);

CREATE TABLE [CoursesEquipmentTypes] (
    [AcademicYearId] uniqueidentifier NOT NULL,
    [CourseId] uniqueidentifier NOT NULL,
    [EquipmentTypeId] uniqueidentifier NOT NULL,
    [UniversityId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CoursesEquipmentTypes] PRIMARY KEY ([EquipmentTypeId], [CourseId], [UniversityId], [AcademicYearId]),
    CONSTRAINT [FK_CoursesEquipmentTypes_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoursesEquipmentTypes_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoursesEquipmentTypes_EquipmentTypes_EquipmentTypeId] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [EquipmentTypes] ([EquipmentTypeId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CoursesEquipmentTypes_Universities_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [Universities] ([UniversityId]) ON DELETE CASCADE
);

CREATE TABLE [Sites] (
    [SiteId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UniversityId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Sites] PRIMARY KEY ([SiteId]),
    CONSTRAINT [FK_Sites_Universities_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [Universities] ([UniversityId])
);

CREATE TABLE [Classrooms] (
    [ClassroomId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    [Capacity] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [SiteId] uniqueidentifier NULL,
    CONSTRAINT [PK_Classrooms] PRIMARY KEY ([ClassroomId]),
    CONSTRAINT [FK_Classrooms_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE CASCADE
);

CREATE TABLE [SitesCoursesSchedules] (
    [SiteId] uniqueidentifier NOT NULL,
    [ScheduleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SitesCoursesSchedules] PRIMARY KEY ([SiteId], [ScheduleId]),
    CONSTRAINT [FK_SitesCoursesSchedules_CoursesSchedules_ScheduleId] FOREIGN KEY ([ScheduleId]) REFERENCES [CoursesSchedules] ([ScheduleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SitesCoursesSchedules_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE CASCADE
);

CREATE TABLE [StudentGroups] (
    [StudentGroupId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Code] nvarchar(450) NOT NULL,
    [NumberOfStudents] int NOT NULL,
    [OptionGrade] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [AcademicYearId] uniqueidentifier NULL,
    [SiteId] uniqueidentifier NULL,
    [OptionId] uniqueidentifier NULL,
    CONSTRAINT [PK_StudentGroups] PRIMARY KEY ([StudentGroupId]),
    CONSTRAINT [FK_StudentGroups_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE SET NULL,
    CONSTRAINT [FK_StudentGroups_Options_OptionId] FOREIGN KEY ([OptionId]) REFERENCES [Options] ([OptionId]) ON DELETE SET NULL,
    CONSTRAINT [FK_StudentGroups_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE SET NULL
);

CREATE TABLE [UniversitiesSitesEquipments] (
    [EquipmentId] uniqueidentifier NOT NULL,
    [SiteId] uniqueidentifier NULL,
    [UniversityId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UniversitiesSitesEquipments] PRIMARY KEY ([EquipmentId]),
    CONSTRAINT [FK_UniversitiesSitesEquipments_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([EquipmentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UniversitiesSitesEquipments_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([SiteId]) ON DELETE SET NULL,
    CONSTRAINT [FK_UniversitiesSitesEquipments_Universities_UniversityId] FOREIGN KEY ([UniversityId]) REFERENCES [Universities] ([UniversityId]) ON DELETE CASCADE
);

CREATE TABLE [ClassroomsEquipments] (
    [AcademicYearId] uniqueidentifier NOT NULL,
    [EquipmentId] uniqueidentifier NOT NULL,
    [ClassroomId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ClassroomsEquipments] PRIMARY KEY ([EquipmentId], [ClassroomId], [AcademicYearId]),
    CONSTRAINT [FK_ClassroomsEquipments_AcademicYears_AcademicYearId] FOREIGN KEY ([AcademicYearId]) REFERENCES [AcademicYears] ([AcademicYearId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClassroomsEquipments_Classrooms_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [Classrooms] ([ClassroomId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ClassroomsEquipments_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([EquipmentId]) ON DELETE CASCADE
);

CREATE TABLE [Sessions] (
    [SessionId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] nvarchar(max) NOT NULL,
    [DatetimeStart] datetime2 NOT NULL,
    [DatetimeEnd] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [UpdatedAt] datetime2 NOT NULL DEFAULT (GETDATE()),
    [ClassroomId] uniqueidentifier NOT NULL,
    [CourseId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY ([SessionId]),
    CONSTRAINT [FK_Sessions_Classrooms_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [Classrooms] ([ClassroomId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Sessions_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([CourseId]) ON DELETE CASCADE
);

CREATE TABLE [EquipmentSessions] (
    [EquipmentId] uniqueidentifier NOT NULL,
    [SessionId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_EquipmentSessions] PRIMARY KEY ([EquipmentId], [SessionId]),
    CONSTRAINT [FK_EquipmentSessions_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([EquipmentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_EquipmentSessions_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([SessionId]) ON DELETE CASCADE
);

CREATE TABLE [StudentGroupSessions] (
    [StudentGroupId] uniqueidentifier NOT NULL,
    [SessionId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_StudentGroupSessions] PRIMARY KEY ([StudentGroupId], [SessionId]),
    CONSTRAINT [FK_StudentGroupSessions_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([SessionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentGroupSessions_StudentGroups_StudentGroupId] FOREIGN KEY ([StudentGroupId]) REFERENCES [StudentGroups] ([StudentGroupId]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Classrooms_Code] ON [Classrooms] ([Code]);

CREATE INDEX [IX_Classrooms_SiteId] ON [Classrooms] ([SiteId]);

CREATE INDEX [IX_ClassroomsEquipments_AcademicYearId] ON [ClassroomsEquipments] ([AcademicYearId]);

CREATE INDEX [IX_ClassroomsEquipments_ClassroomId] ON [ClassroomsEquipments] ([ClassroomId]);

CREATE UNIQUE INDEX [IX_Courses_Code] ON [Courses] ([Code]);

CREATE INDEX [IX_CoursesEquipmentTypes_AcademicYearId] ON [CoursesEquipmentTypes] ([AcademicYearId]);

CREATE INDEX [IX_CoursesEquipmentTypes_CourseId] ON [CoursesEquipmentTypes] ([CourseId]);

CREATE INDEX [IX_CoursesEquipmentTypes_UniversityId] ON [CoursesEquipmentTypes] ([UniversityId]);

CREATE UNIQUE INDEX [IX_CoursesSchedules_DayOfTheWeek_HourStart_HourEnd] ON [CoursesSchedules] ([DayOfTheWeek], [HourStart], [HourEnd]);

CREATE INDEX [IX_DaysWithoutCourse_AcademicYearId] ON [DaysWithoutCourse] ([AcademicYearId]);

CREATE UNIQUE INDEX [IX_DaysWithoutCourse_Date] ON [DaysWithoutCourse] ([Date]);

CREATE INDEX [IX_Equipments_EquipmentTypeId] ON [Equipments] ([EquipmentTypeId]);

CREATE INDEX [IX_EquipmentSessions_SessionId] ON [EquipmentSessions] ([SessionId]);

CREATE UNIQUE INDEX [IX_EquipmentTypes_Code] ON [EquipmentTypes] ([Code]);

CREATE UNIQUE INDEX [IX_EquipmentTypes_Name] ON [EquipmentTypes] ([Name]);

CREATE INDEX [IX_OptionCourse_AcademicYearId] ON [OptionCourse] ([AcademicYearId]);

CREATE INDEX [IX_OptionCourse_OptionId] ON [OptionCourse] ([OptionId]);

CREATE UNIQUE INDEX [IX_Options_Code] ON [Options] ([Code]);

CREATE UNIQUE INDEX [IX_Sessions_ClassroomId_DatetimeStart_DatetimeEnd] ON [Sessions] ([ClassroomId], [DatetimeStart], [DatetimeEnd]);

CREATE INDEX [IX_Sessions_CourseId] ON [Sessions] ([CourseId]);

CREATE UNIQUE INDEX [IX_Sites_Code] ON [Sites] ([Code]);

CREATE INDEX [IX_Sites_UniversityId] ON [Sites] ([UniversityId]);

CREATE INDEX [IX_SitesCoursesSchedules_ScheduleId] ON [SitesCoursesSchedules] ([ScheduleId]);

CREATE INDEX [IX_StudentGroups_AcademicYearId] ON [StudentGroups] ([AcademicYearId]);

CREATE UNIQUE INDEX [IX_StudentGroups_Code] ON [StudentGroups] ([Code]);

CREATE INDEX [IX_StudentGroups_OptionId] ON [StudentGroups] ([OptionId]);

CREATE INDEX [IX_StudentGroups_SiteId] ON [StudentGroups] ([SiteId]);

CREATE INDEX [IX_StudentGroupSessions_SessionId] ON [StudentGroupSessions] ([SessionId]);

CREATE UNIQUE INDEX [IX_Universities_Code] ON [Universities] ([Code]);

CREATE UNIQUE INDEX [IX_Universities_Name] ON [Universities] ([Name]);

CREATE UNIQUE INDEX [IX_Universities_Phone] ON [Universities] ([Phone]);

CREATE INDEX [IX_UniversitiesSitesEquipments_SiteId] ON [UniversitiesSitesEquipments] ([SiteId]);

CREATE INDEX [IX_UniversitiesSitesEquipments_UniversityId] ON [UniversitiesSitesEquipments] ([UniversityId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250320201327_InitialCreate', N'9.0.2');

COMMIT;
GO

