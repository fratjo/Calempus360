using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
using Newtonsoft.Json;
using Xunit.Abstractions;
using DayOfWeek = Calempus360.Core.Models.DayOfWeek;

namespace Calempus360.Test.Infrastructure.Persistence.Mappers;

public class ModelMappersTest(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void EquipmentType_ToDomain_Test()
    {
        // Arrange

        var equipmentTypeEntity = new EquipmentTypeEntity
        {
            EquipmentTypeId = Guid.NewGuid(),
            Name = "Projector",
            Code = "PRJ",
            Description = "Projector",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // Act

        var equipmentType = equipmentTypeEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(equipmentType));

        // Assert

        Assert.NotNull(equipmentType);
        Assert.Equal(equipmentTypeEntity.EquipmentTypeId, equipmentType.Id);
        Assert.Equal(equipmentTypeEntity.Name, equipmentType.Name);
        Assert.Equal(equipmentTypeEntity.Code, equipmentType.Code);
        Assert.Equal(equipmentTypeEntity.Description, equipmentType.Description);
        Assert.Equal(equipmentTypeEntity.CreatedAt, equipmentType.CreatedAt);
        Assert.Equal(equipmentTypeEntity.UpdatedAt, equipmentType.UpdatedAt);
    }

    [Fact]
    public void EquipmentType_ToEntity_Test()
    {
        // Arrange

        var equipmentType = new EquipmentType(
            id: Guid.NewGuid(),
            name: "Projector",
            code: "PRJ",
            description: "Projector",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now
        );

        // Act

        var equipmentTypeEntity = equipmentType.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(equipmentTypeEntity));

        // Assert

        Assert.NotNull(equipmentTypeEntity);
        Assert.Equal(equipmentType.Id, equipmentTypeEntity.EquipmentTypeId);
        Assert.Equal(equipmentType.Name, equipmentTypeEntity.Name);
        Assert.Equal(equipmentType.Code, equipmentTypeEntity.Code);
        Assert.Equal(equipmentType.Description, equipmentTypeEntity.Description);
        Assert.Equal(equipmentType.CreatedAt, equipmentTypeEntity.CreatedAt);
        Assert.Equal(equipmentType.UpdatedAt, equipmentTypeEntity.UpdatedAt);
    }

    [Fact]
    public void Equipment_ToDomain_Test()
    {
        // Arrange

        var equipmentTypeEntity = new EquipmentTypeEntity
        {
            EquipmentTypeId = Guid.NewGuid(),
            Name = "Projector",
            Code = "PRJ",
            Description = "Projector",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var equipmentEntity = new EquipmentEntity
        {
            EquipmentId = Guid.NewGuid(),
            Name = "Projector 1",
            Code = "PRJ1",
            Brand = "Brand 1",
            Model = "Model 1",
            Description = "Projector 1",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            EquipmentTypeEntity = equipmentTypeEntity
        };

        // Act

        var equipment = equipmentEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(equipment));

        // Assert

        Assert.NotNull(equipment);
        Assert.Equal(equipmentEntity.EquipmentId, equipment.Id);
        Assert.Equal(equipmentEntity.Name, equipment.Name);
        Assert.Equal(equipmentEntity.Code, equipment.Code);
        Assert.Equal(equipmentEntity.Brand, equipment.Brand);
        Assert.Equal(equipmentEntity.Model, equipment.Model);
        Assert.Equal(equipmentEntity.Description, equipment.Description);
        Assert.Equal(equipmentEntity.CreatedAt, equipment.CreatedAt);
        Assert.Equal(equipmentEntity.UpdatedAt, equipment.UpdatedAt);
        Assert.NotNull(equipment.EquipmentType);
        Assert.Equal(equipmentTypeEntity.EquipmentTypeId, equipment.EquipmentType.Id);
    }

    [Fact]
    public void Equipment_ToEntity_Test()
    {
        // Arrange

        var equipmentType = new EquipmentType(
            id: Guid.NewGuid(),
            name: "Projector",
            code: "PRJ",
            description: "Projector",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now
        );

        var equipment = new Equipment(
            id: Guid.NewGuid(),
            name: "Projector 1",
            code: "PRJ1",
            brand: "Brand 1",
            model: "Model 1",
            description: "Projector 1",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipmentType: equipmentType
        );

        // Act

        var equipmentEntity = equipment.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(equipmentEntity));

        // Assert

        Assert.NotNull(equipmentEntity);
        Assert.Equal(equipment.Id, equipmentEntity.EquipmentId);
        Assert.Equal(equipment.Name, equipmentEntity.Name);
        Assert.Equal(equipment.Code, equipmentEntity.Code);
        Assert.Equal(equipment.Brand, equipmentEntity.Brand);
        Assert.Equal(equipment.Model, equipmentEntity.Model);
        Assert.Equal(equipment.Description, equipmentEntity.Description);
        Assert.Equal(equipment.CreatedAt, equipmentEntity.CreatedAt);
        Assert.Equal(equipment.UpdatedAt, equipmentEntity.UpdatedAt);
        Assert.Equal(equipmentType.Id, equipmentEntity.EquipmentTypeId);
    }

    [Fact]
    public void Classroom_ToDomain_Test()
    {
        // Arrange

        var equipmentTypeEntity = new EquipmentTypeEntity
        {
            EquipmentTypeId = Guid.NewGuid(),
            Name = "Projector",
            Code = "PRJ",
            Description = "Projector",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var equipmentEntity = new EquipmentEntity
        {
            EquipmentId = Guid.NewGuid(),
            Name = "Projector 1",
            Code = "PRJ1",
            Brand = "Brand 1",
            Model = "Model 1",
            Description = "Projector 1",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            EquipmentTypeEntity = equipmentTypeEntity
        };

        var classroomEntity = new ClassroomEntity
        {
            ClassroomId = Guid.NewGuid(),
            Name = "Classroom 1",
            Code = "CR1",
            Capacity = 50,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            ClassroomEquipments = new List<ClassroomEquipmentEntity>
            {
                new ClassroomEquipmentEntity
                {
                    EquipmentEntity = equipmentEntity
                }
            }
        };

        // Act

        var classroom = classroomEntity.ToDomainModel(true);

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(classroom));

        // Assert

        Assert.NotNull(classroom);
        Assert.Equal(classroomEntity.ClassroomId, classroom.Id);
        Assert.Equal(classroomEntity.Name, classroom.Name);
        Assert.Equal(classroomEntity.Code, classroom.Code);
        Assert.Equal(classroomEntity.Capacity, classroom.Capacity);
        Assert.Equal(classroomEntity.CreatedAt, classroom.CreatedAt);
        Assert.Equal(classroomEntity.UpdatedAt, classroom.UpdatedAt);
        Assert.NotNull(classroom.Equipments!);
        Assert.Single(classroom.Equipments);
        Assert.Equal(equipmentEntity.EquipmentId, classroom.Equipments.First().Id);
    }

    [Fact]
    public void Classroom_ToEntity_Test()
    {
        // Arrange

        var equipmentType = new EquipmentType(
            id: Guid.NewGuid(),
            name: "Projector",
            code: "PRJ",
            description: "Projector",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now
        );

        var equipment = new Equipment(
            id: Guid.NewGuid(),
            name: "Projector 1",
            code: "PRJ1",
            brand: "Brand 1",
            model: "Model 1",
            description: "Projector 1",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipmentType: equipmentType
        );

        var classroom = new Classroom(
            id: Guid.NewGuid(),
            name: "Classroom 1",
            code: "CR1",
            capacity: 50,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipments: new List<Equipment>
            {
                equipment
            }
        );

        // Act

        var classroomEntity = classroom.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(classroomEntity));

        // Assert

        Assert.NotNull(classroomEntity);
        Assert.Equal(classroom.Id, classroomEntity.ClassroomId);
        Assert.Equal(classroom.Name, classroomEntity.Name);
        Assert.Equal(classroom.Code, classroomEntity.Code);
        Assert.Equal(classroom.Capacity, classroomEntity.Capacity);
        Assert.Equal(classroom.CreatedAt, classroomEntity.CreatedAt);
        Assert.Equal(classroom.UpdatedAt, classroomEntity.UpdatedAt);
    }

    [Fact]
    public void Course_ToDomain_Test()
    {
        // Arrange

        var equipmentTypeEntity = new EquipmentTypeEntity
        {
            EquipmentTypeId = Guid.NewGuid(),
            Name = "Projector",
            Code = "PRJ",
            Description = "Projector",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var courseEntity = new CourseEntity
        {
            CourseId = Guid.NewGuid(),
            Name = "Course 1",
            Code = "C1",
            Description = "Course 1",
            WeeklyHours = 3,
            TotalHours = 15,
            Semester = "1",
            Credits = 3,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            EquipmentTypes = new List<CourseEquipmentTypeEntity>
            {
                new CourseEquipmentTypeEntity
                {
                    EquipmentTypeEntity = equipmentTypeEntity
                }
            }
        };

        // Act

        var course = courseEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(course));

        // Assert

        Assert.NotNull(course);
        Assert.Equal(courseEntity.CourseId, course.Id);
        Assert.Equal(courseEntity.Name, course.Name);
        Assert.Equal(courseEntity.Code, course.Code);
        Assert.Equal(courseEntity.Description, course.Description);
        Assert.Equal(courseEntity.WeeklyHours, course.WeeklyHours);
        Assert.Equal(courseEntity.TotalHours, course.TotalHours);
        Assert.Equal(courseEntity.Semester, course.Semester);
        Assert.Equal(courseEntity.Credits, course.Credits);
        Assert.Equal(courseEntity.CreatedAt, course.CreatedAt);
        Assert.Equal(courseEntity.UpdatedAt, course.UpdatedAt);
        Assert.NotNull(course.EquipmentTypes);
        Assert.Single(course.EquipmentTypes);
        Assert.Equal(equipmentTypeEntity.EquipmentTypeId, course.EquipmentTypes.First().Id);
    }

    [Fact]
    public void Course_ToEntity_Test()
    {
        // Arrange

        var course = new Course(
            id: Guid.NewGuid(),
            name: "Course 1",
            code: "C1",
            description: "Course 1",
            weeklyHours: 3,
            totalHours: 15,
            semester: "1",
            credits: 3,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipmentTypes: null
        );

        // Act

        var courseEntity = course.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(courseEntity));

        // Assert

        Assert.NotNull(courseEntity);
        Assert.Equal(course.Id, courseEntity.CourseId);
        Assert.Equal(course.Name, courseEntity.Name);
        Assert.Equal(course.Code, courseEntity.Code);
        Assert.Equal(course.Description, courseEntity.Description);
        Assert.Equal(course.WeeklyHours, courseEntity.WeeklyHours);
        Assert.Equal(course.TotalHours, courseEntity.TotalHours);
        Assert.Equal(course.Semester, courseEntity.Semester);
        Assert.Equal(course.Credits, courseEntity.Credits);
        Assert.Equal(course.CreatedAt, courseEntity.CreatedAt);
        Assert.Equal(course.UpdatedAt, courseEntity.UpdatedAt);
    }

    [Fact]
    public void Option_ToDomain_Test()
    {
        // Arrange

        var equipmentTypeEntity = new EquipmentTypeEntity
        {
            EquipmentTypeId = Guid.NewGuid(),
            Name = "Projector",
            Code = "PRJ",
            Description = "Projector",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var courseEntity = new CourseEntity
        {
            CourseId = Guid.NewGuid(),
            Name = "Course 1",
            Code = "C1",
            Description = "Course 1",
            WeeklyHours = 3,
            TotalHours = 15,
            Semester = "1",
            Credits = 3,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            EquipmentTypes = new List<CourseEquipmentTypeEntity>
            {
                new CourseEquipmentTypeEntity
                {
                    EquipmentTypeEntity = equipmentTypeEntity
                }
            }
        };

        var optionEntity = new OptionEntity
        {
            OptionId = Guid.NewGuid(),
            Name = "Option 1",
            Code = "O1",
            Description = "Option 1",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            OptionCourses = new List<OptionCourseEntity>
            {
                new OptionCourseEntity
                {
                    CourseEntity = courseEntity
                }
            }
        };

        // Act

        var option = optionEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(option));

        // Assert

        Assert.NotNull(option);
        Assert.Equal(optionEntity.OptionId, option.Id);
        Assert.Equal(optionEntity.Name, option.Name);
        Assert.Equal(optionEntity.Code, option.Code);
        Assert.Equal(optionEntity.Description, option.Description);
        Assert.Equal(optionEntity.CreatedAt, option.CreatedAt);
        Assert.Equal(optionEntity.UpdatedAt, option.UpdatedAt);
        Assert.NotNull(option.Courses);
        Assert.Single(option.Courses);
        Assert.Equal(courseEntity.CourseId, option.Courses.First().Id);
    }

    [Fact]
    public void Option_ToEntity_Test()
    {
        // Arrange

        var course = new Course(
            id: Guid.NewGuid(),
            name: "Course 1",
            code: "C1",
            description: "Course 1",
            weeklyHours: 3,
            totalHours: 15,
            semester: "1",
            credits: 3,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipmentTypes: null
        );

        var option = new Option(
            id: Guid.NewGuid(),
            name: "Option 1",
            code: "O1",
            description: "Option 1",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            courses: new List<Course>
            {
                course
            }
        );

        // Act

        var optionEntity = option.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(optionEntity));

        // Assert

        Assert.NotNull(optionEntity);
        Assert.Equal(option.Id, optionEntity.OptionId);
        Assert.Equal(option.Name, optionEntity.Name);
        Assert.Equal(option.Code, optionEntity.Code);
        Assert.Equal(option.Description, optionEntity.Description);
        Assert.Equal(option.CreatedAt, optionEntity.CreatedAt);
        Assert.Equal(option.UpdatedAt, optionEntity.UpdatedAt);
    }

    [Fact]
    public void StudentGroup_ToDomain_Test()
    {
        // Arrange

        var optionEntity = new OptionEntity
        {
            OptionId = Guid.NewGuid(),
            Name = "Option 1",
            Code = "O1",
            Description = "Option 1",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var siteEntity = new SiteEntity
        {
            SiteId = Guid.NewGuid(),
            Name = "Site 1",
            Code = "S1",
            Address = "Address 1",
            Phone = "1234567890",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var studentGroupEntity = new StudentGroupEntity
        {
            StudentGroupId = Guid.NewGuid(),
            Code = "G1",
            NumberOfStudents = 50,
            OptionGrade = 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            OptionEntity = optionEntity,
            SiteEntity = siteEntity
        };

        // Act

        var studentGroup = studentGroupEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(studentGroup));

        // Assert

        Assert.NotNull(studentGroup);
        Assert.Equal(studentGroupEntity.StudentGroupId, studentGroup.Id);
        Assert.Equal(studentGroupEntity.Code, studentGroup.Code);
        Assert.Equal(studentGroupEntity.NumberOfStudents, studentGroup.NumberOfStudents);
        Assert.Equal(studentGroupEntity.OptionGrade, studentGroup.OptionGrade);
        Assert.Equal(studentGroupEntity.CreatedAt, studentGroup.CreatedAt);
        Assert.Equal(studentGroupEntity.UpdatedAt, studentGroup.UpdatedAt);
        Assert.NotNull(studentGroup.Site);
        Assert.Equal(siteEntity.SiteId, studentGroup.Site.Id);
        Assert.NotNull(studentGroup.Option);
        Assert.Equal(optionEntity.OptionId, studentGroup.Option.Id);
    }

    [Fact]
    public void StudentGroup_ToEntity_Test()
    {
        // Arrange

        var option = new Option(
            id: Guid.NewGuid(),
            name: "Option 1",
            code: "O1",
            description: "Option 1",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            courses: null
        );

        var site = new Site(
            id: Guid.NewGuid(),
            name: "Site 1",
            code: "S1",
            address: "Address 1",
            phone: "1234567890",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            classrooms: null,
            schedules: null,
            equipments: null
        );

        var studentGroup = new StudentGroup(
            id: Guid.NewGuid(),
            code: "G1",
            numberOfStudents: 50,
            optionGrade: 1,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            site: site,
            option: option
        );

        // Act

        var studentGroupEntity = studentGroup.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(studentGroupEntity));

        // Assert

        Assert.NotNull(studentGroupEntity);
        Assert.Equal(studentGroup.Id, studentGroupEntity.StudentGroupId);
        Assert.Equal(studentGroup.Code, studentGroupEntity.Code);
        Assert.Equal(studentGroup.NumberOfStudents, studentGroupEntity.NumberOfStudents);
        Assert.Equal(studentGroup.OptionGrade, studentGroupEntity.OptionGrade);
        Assert.Equal(studentGroup.CreatedAt, studentGroupEntity.CreatedAt);
        Assert.Equal(studentGroup.UpdatedAt, studentGroupEntity.UpdatedAt);
    }

    [Fact]
    public void Session_ToDomain_Test()
    {
        // Arrange

        var courseEntity = new CourseEntity
        {
            CourseId = Guid.NewGuid(),
            Name = "Course 1",
            Code = "C1",
            Description = "Course 1",
            WeeklyHours = 3,
            TotalHours = 15,
            Semester = "1",
            Credits = 3,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var classroomEntity = new ClassroomEntity
        {
            ClassroomId = Guid.NewGuid(),
            Name = "Classroom 1",
            Code = "CR1",
            Capacity = 50,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var equipmentTypeEntity = new EquipmentTypeEntity
        {
            EquipmentTypeId = Guid.NewGuid(),
            Name = "Projector",
            Code = "PRJ",
            Description = "Projector",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var equipmentEntity = new EquipmentEntity
        {
            EquipmentId = Guid.NewGuid(),
            Name = "Projector 1",
            Code = "PRJ1",
            Brand = "Brand 1",
            Model = "Model 1",
            Description = "Projector 1",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            EquipmentTypeEntity = equipmentTypeEntity
        };

        var siteEntity = new SiteEntity
        {
            SiteId = Guid.NewGuid(),
            Name = "Site 1",
            Code = "S1",
            Address = "Address 1",
            Phone = "1234567890",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var optionEntity = new OptionEntity
        {
            OptionId = Guid.NewGuid(),
            Name = "Option 1",
            Code = "O1",
            Description = "Option 1",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var studentGroupEntity = new StudentGroupEntity
        {
            StudentGroupId = Guid.NewGuid(),
            Code = "G1",
            NumberOfStudents = 50,
            OptionGrade = 1,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            OptionEntity = optionEntity,
            SiteEntity = siteEntity
        };

        var sessionEntity = new SessionEntity
        {
            SessionId = Guid.NewGuid(),
            DatetimeStart = DateTime.Now,
            DatetimeEnd = DateTime.Now,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CourseEntity = courseEntity,
            ClassroomEntity = classroomEntity,
            EquipmentSessions = new List<EquipmentSessionEntity>
            {
                new EquipmentSessionEntity
                {
                    EquipmentEntity = equipmentEntity
                }
            },
            StudentGroupSessions = new List<StudentGroupSessionEntity>
            {
                new StudentGroupSessionEntity
                {
                    StudentGroupEntity = studentGroupEntity
                }
            }
        };

        // Act

        var session = sessionEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(session));

        // Assert

        Assert.NotNull(session);
        Assert.Equal(sessionEntity.SessionId, session.Id);
        Assert.Equal(sessionEntity.DatetimeStart, session.DateTimeStart);
        Assert.Equal(sessionEntity.DatetimeEnd, session.DateTimeEnd);
        Assert.Equal(sessionEntity.CreatedAt, session.CreatedAt);
        Assert.Equal(sessionEntity.UpdatedAt, session.UpdatedAt);
        Assert.NotNull(session.Course);
        Assert.Equal(courseEntity.CourseId, session.Course.Id);
        Assert.NotNull(session.Classroom);
        Assert.Equal(classroomEntity.ClassroomId, session.Classroom.Id);
        Assert.NotNull(session.Equipments);
        Assert.Single(session.Equipments);
        Assert.Equal(equipmentEntity.EquipmentId, session.Equipments.First().Id);
        Assert.NotNull(session.StudentGroups);
        Assert.Single(session.StudentGroups);
        Assert.Equal(studentGroupEntity.StudentGroupId, session.StudentGroups.First().Id);
    }

    [Fact]
    public void Session_ToEntity_Test()
    {
        // Arrange

        var course = new Course(
            id: Guid.NewGuid(),
            name: "Course 1",
            code: "C1",
            description: "Course 1",
            weeklyHours: 3,
            totalHours: 15,
            semester: "1",
            credits: 3,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipmentTypes: null
        );

        var classroom = new Classroom(
            id: Guid.NewGuid(),
            name: "Classroom 1",
            code: "CR1",
            capacity: 50,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipments: null
        );

        var equipmentType = new EquipmentType(
            id: Guid.NewGuid(),
            name: "Projector",
            code: "PRJ",
            description: "Projector",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now
        );

        var equipment = new Equipment(
            id: Guid.NewGuid(),
            name: "Projector 1",
            code: "PRJ1",
            brand: "Brand 1",
            model: "Model 1",
            description: "Projector 1",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            equipmentType: equipmentType
        );

        var site = new Site(
            id: Guid.NewGuid(),
            name: "Site 1",
            code: "S1",
            address: "Address 1",
            phone: "1234567890",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            classrooms: null,
            schedules: null,
            equipments: null
        );

        var option = new Option(
            id: Guid.NewGuid(),
            name: "Option 1",
            code: "O1",
            description: "Option 1",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            courses: null
        );

        var studentGroup = new StudentGroup(
            id: Guid.NewGuid(),
            code: "G1",
            numberOfStudents: 50,
            optionGrade: 1,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            site: site,
            option: option
        );

        var session = new Session(
            id: Guid.NewGuid(),
            dateTimeStart: DateTime.Now,
            dateTimeEnd: DateTime.Now,
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            course: course,
            classroom: classroom,
            equipments: new List<Equipment>
            {
                equipment
            },
            studentGroups: new List<StudentGroup>
            {
                studentGroup
            },
            name: "Session 1"
        );

        // Act

        var sessionEntity = session.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(sessionEntity));

        // Assert

        Assert.NotNull(sessionEntity);
        Assert.Equal(session.Id, sessionEntity.SessionId);
        Assert.Equal(session.DateTimeStart, sessionEntity.DatetimeStart);
        Assert.Equal(session.DateTimeEnd, sessionEntity.DatetimeEnd);
        Assert.Equal(session.CreatedAt, sessionEntity.CreatedAt);
        Assert.Equal(session.UpdatedAt, sessionEntity.UpdatedAt);
    }

    [Fact]
    public void Schedule_ToDomain_Test()
    {
        // Arrange

        var scheduleEntity = new CourseScheduleEntity
        {
            ScheduleId = Guid.NewGuid(),
            HourStart = TimeOnly.FromDateTime(DateTime.Now),
            HourEnd = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // Act

        var schedule = scheduleEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(schedule));

        // Assert

        Assert.NotNull(schedule);
        Assert.Equal(scheduleEntity.ScheduleId, schedule.Id);
        Assert.Equal(scheduleEntity.HourStart, schedule.TimeStart);
        Assert.Equal(scheduleEntity.HourEnd, schedule.TimeEnd);
    }

    [Fact]
    public void Schedule_ToEntity_Test()
    {
        // Arrange

        var schedule = new Schedule(
            id: Guid.NewGuid(),
            dayOfWeek: DayOfWeek.Monday,
            timeStart: TimeOnly.FromDateTime(DateTime.Now),
            timeEnd: TimeOnly.FromDateTime(DateTime.Now.AddHours(1))
        );

        // Act

        var scheduleEntity = schedule.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(scheduleEntity));

        // Assert

        Assert.NotNull(scheduleEntity);
        Assert.Equal(schedule.Id, scheduleEntity.ScheduleId);
        Assert.Equal((int)schedule.DayOfWeek, scheduleEntity.DayOfTheWeek);
        Assert.Equal(schedule.TimeStart, scheduleEntity.HourStart);
        Assert.Equal(schedule.TimeEnd, scheduleEntity.HourEnd);
    }

    [Fact]
    public void Site_ToDomain_Test()
    {
        // Arrange
        var siteEntity = new SiteEntity
        {
            SiteId = Guid.NewGuid(),
            Name = "Site 1",
            Code = "S1",
            Address = "Address 1",
            Phone = "1234567890",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // Act
        var site = siteEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(site));

        // Assert
        Assert.NotNull(site);
        Assert.Equal(siteEntity.SiteId, site.Id);
        Assert.Equal(siteEntity.Name, site.Name);
        Assert.Equal(siteEntity.Code, site.Code);
        Assert.Equal(siteEntity.Address, site.Address);
        Assert.Equal(siteEntity.Phone, site.Phone);
        Assert.Equal(siteEntity.CreatedAt, site.CreatedAt);
        Assert.Equal(siteEntity.UpdatedAt, site.UpdatedAt);
    }

    [Fact]
    public void Site_ToEntity_Test()
    {
        // Arrange
        var site = new Site(
            id: Guid.NewGuid(),
            name: "Site 1",
            code: "S1",
            address: "Address 1",
            phone: "1234567890",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            classrooms: null,
            schedules: null,
            equipments: null
        );

        // Act
        var siteEntity = site.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(siteEntity));

        // Assert
        Assert.NotNull(siteEntity);
        Assert.Equal(site.Id, siteEntity.SiteId);
        Assert.Equal(site.Name, siteEntity.Name);
        Assert.Equal(site.Code, siteEntity.Code);
        Assert.Equal(site.Address, siteEntity.Address);
        Assert.Equal(site.Phone, siteEntity.Phone);
        Assert.Equal(site.CreatedAt, siteEntity.CreatedAt);
        Assert.Equal(site.UpdatedAt, siteEntity.UpdatedAt);
    }

    [Fact]
    public void University_ToDomain_Test()
    {
        // Arrange
        var universityEntity = new UniversityEntity
        {
            UniversityId = Guid.NewGuid(),
            Name = "University 1",
            Code = "U1",
            Address = "Address 1",
            Phone = "1234567890",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // Act
        var university = universityEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(university));

        // Assert
        Assert.NotNull(university);
        Assert.Equal(universityEntity.UniversityId, university.Id);
        Assert.Equal(universityEntity.Name, university.Name);
        Assert.Equal(universityEntity.Code, university.Code);
        Assert.Equal(universityEntity.Address, university.Address);
        Assert.Equal(universityEntity.Phone, university.Phone);
        Assert.Equal(universityEntity.CreatedAt, university.CreatedAt);
        Assert.Equal(universityEntity.UpdatedAt, university.UpdatedAt);
    }

    [Fact]
    public void University_ToEntity_Test()
    {
        // Arrange
        var university = new University(
            id: Guid.NewGuid(),
            name: "University 1",
            code: "U1",
            address: "Address 1",
            phone: "1234567890",
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            sites: null,
            equipments: null
        );

        // Act
        var universityEntity = university.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(universityEntity));

        // Assert
        Assert.NotNull(universityEntity);
        Assert.Equal(university.Id, universityEntity.UniversityId);
        Assert.Equal(university.Name, universityEntity.Name);
        Assert.Equal(university.Code, universityEntity.Code);
        Assert.Equal(university.Address, universityEntity.Address);
        Assert.Equal(university.Phone, universityEntity.Phone);
        Assert.Equal(university.CreatedAt, universityEntity.CreatedAt);
        Assert.Equal(university.UpdatedAt, universityEntity.UpdatedAt);
    }

    [Fact]
    public void DayWithoutCourse_ToDomain_Test()
    {
        // Arrange
        var dayWithoutCourseEntity = new DayWithoutCourseEntity
        {
            DayWithoutCourseId = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(DateTime.Now),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // Act
        var dayWithoutCourse = dayWithoutCourseEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(dayWithoutCourse));

        // Assert
        Assert.NotNull(dayWithoutCourse);
        Assert.Equal(dayWithoutCourseEntity.DayWithoutCourseId, dayWithoutCourse.Id);
        Assert.Equal(dayWithoutCourseEntity.Date, dayWithoutCourse.Date);
        Assert.Equal(dayWithoutCourseEntity.CreatedAt, dayWithoutCourse.CreatedAt);
        Assert.Equal(dayWithoutCourseEntity.UpdatedAt, dayWithoutCourse.UpdatedAt);
    }

    [Fact]
    public void DayWithoutCourse_ToEntity_Test()
    {
        // Arrange
        var dayWithoutCourse = new DayWithoutCourse(
            id: Guid.NewGuid(),
            name: "Day Without Course 1",
            date: DateOnly.FromDateTime(DateTime.Now),
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now
        );

        // Act
        var dayWithoutCourseEntity = dayWithoutCourse.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(dayWithoutCourseEntity));

        // Assert
        Assert.NotNull(dayWithoutCourseEntity);
        Assert.Equal(dayWithoutCourse.Id, dayWithoutCourseEntity.DayWithoutCourseId);
        Assert.Equal(dayWithoutCourse.Date, dayWithoutCourseEntity.Date);
        Assert.Equal(dayWithoutCourse.CreatedAt, dayWithoutCourseEntity.CreatedAt);
        Assert.Equal(dayWithoutCourse.UpdatedAt, dayWithoutCourseEntity.UpdatedAt);
    }

    [Fact]
    public void AcademicYear_ToDomain_Test()
    {
        // Arrange

        var dayWithoutCourseEntity = new DayWithoutCourseEntity
        {
            DayWithoutCourseId = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(DateTime.Now),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var academicYearEntity = new AcademicYearEntity
        {
            AcademicYearId = Guid.NewGuid(),
            AcademicYearCode = "2023-2024",
            DateStart = DateOnly.FromDateTime(DateTime.Now),
            DateEnd = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            DaysWithoutCourses = new List<DayWithoutCourseEntity>
            {
                dayWithoutCourseEntity
            }
        };

        // Act

        var academicYear = academicYearEntity.ToDomainModel();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(academicYear));

        // Assert

        Assert.NotNull(academicYear);
        Assert.Equal(academicYearEntity.AcademicYearId, academicYear.Id);
        Assert.Equal(academicYearEntity.DateStart, academicYear.DateStart);
        Assert.Equal(academicYearEntity.DateEnd, academicYear.DateEnd);
        Assert.Equal(academicYearEntity.CreatedAt, academicYear.CreatedAt);
        Assert.Equal(academicYearEntity.UpdatedAt, academicYear.UpdatedAt);
        Assert.NotNull(academicYear.DaysWithoutCourses);
        Assert.Single(academicYear.DaysWithoutCourses);
        Assert.Equal(dayWithoutCourseEntity.DayWithoutCourseId, academicYear.DaysWithoutCourses.First().Id);
    }

    [Fact]
    public void AcademicYear_ToEntity_Test()
    {
        // Arrange

        var academicYear = new AcademicYear(
            code: "2023-2024",
            dateStart: DateOnly.FromDateTime(DateTime.Now),
            dateEnd: DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
            createdAt: DateTime.Now,
            updatedAt: DateTime.Now,
            daysWithoutCourses: null
        );

        // Act

        var academicYearEntity = academicYear.ToEntity();

        testOutputHelper.WriteLine(JsonConvert.SerializeObject(academicYearEntity));

        // Assert

        Assert.NotNull(academicYearEntity);
        Assert.Equal(academicYear.Id, academicYearEntity.AcademicYearId);
        Assert.Equal(academicYear.Code, academicYearEntity.AcademicYearCode);
        Assert.Equal(academicYear.DateStart, academicYearEntity.DateStart);
        Assert.Equal(academicYear.DateEnd, academicYearEntity.DateEnd);
        Assert.Equal(academicYear.CreatedAt, academicYearEntity.CreatedAt);
        Assert.Equal(academicYear.UpdatedAt, academicYearEntity.UpdatedAt);
    }
}