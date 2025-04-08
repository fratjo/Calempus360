using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Services.Adapters.ScheduleGenerator;
using ScheduleGenerator;

namespace Calempus360.Test;

public class SchedularGeneratorAdapterTest
{
    [Fact]
    public void Test_ClassAdapter()
    {
        // Arrange

        #region Mock Data

        var classroom = new ClassroomEntity();
        classroom.ClassroomId = new Guid();
        classroom.Name = "Salle de cours A3";
        classroom.Code = "A3";
        classroom.Capacity = 50;
        classroom.CreatedAt = DateTime.Now;
        classroom.UpdatedAt = DateTime.Now;

        var site = new SiteEntity();
        site.SiteId = new Guid();
        site.Name = "Site A";
        site.Code = "A";
        site.CreatedAt = DateTime.Now;
        site.UpdatedAt = DateTime.Now;

        classroom.SiteEntity = site;

        var equipmentType = new EquipmentTypeEntity();
        equipmentType.EquipmentTypeId = new Guid();
        equipmentType.Name = "Retroprojecteur";
        equipmentType.Code = "Projo";
        equipmentType.Description = "Retroprojecteur";
        equipmentType.CreatedAt = DateTime.Now;
        equipmentType.UpdatedAt = DateTime.Now;

        var equipment = new EquipmentEntity();
        equipment.EquipmentId = new Guid();
        equipment.EquipmentTypeEntity = equipmentType;
        equipment.EquipmentTypeId = equipmentType.EquipmentTypeId;
        equipment.Name = "Retroprojecteur Epson EB-2255U";
        equipment.Code = "Projo-EB-2255U";
        equipment.Brand = "Epson";
        equipment.Model = "EB-2255U";
        equipment.Description = "Retroprojecteur Epson EB-2255U";
        equipment.CreatedAt = DateTime.Now;
        equipment.UpdatedAt = DateTime.Now;

        var classroomEquipment = new ClassroomEquipmentEntity();
        classroomEquipment.EquipmentEntity = equipment;
        classroomEquipment.EquipmentId = equipment.EquipmentId;
        classroomEquipment.ClassroomEntity = classroom;
        classroomEquipment.ClassroomId = classroom.ClassroomId;

        equipment.ClassroomEquipments = new() { classroomEquipment };
        classroom.ClassroomEquipments = new() { classroomEquipment };

        #endregion

        // Act

        var result = ClassAdapter.Adapt(classroom);

        var expected = new Class(
            classroom.ClassroomId.ToString()!,
            classroom.SiteEntity.SiteId.ToString()!,
            classroom.Capacity,
            new List<Equipement>
            {
                new Equipement(
                    classroom.SiteEntity.Name,
                    classroomEquipment.EquipmentEntity.EquipmentTypeEntity.Name,
                    classroomEquipment.EquipmentEntity.EquipmentTypeId
                )
            }
        );

        // Assert

        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Site, result.Site);
        Assert.Equal(expected.Capacity, result.Capacity);
        Assert.Equal(expected.Equipments!.Count, result.Equipments!.Count);
    }

    [Fact]
    public void Test_GroupAdapter()
    {
        // Arrange

        #region Mock Data

        var studentGroup = new StudentGroupEntity();
        studentGroup.Code = "G1";
        studentGroup.NumberOfStudents = 30;

        var site = new SiteEntity();
        site.SiteId = new Guid();
        site.Name = "Site A";
        site.Code = "A";

        studentGroup.SiteEntity = site;

        #endregion

        // Act

        var result = GroupAdapter.Adapt(studentGroup);

        var expected = new Group
        {
            Name = studentGroup.StudentGroupId.ToString()!,
            Capacity = studentGroup.NumberOfStudents,
            PreferedSite = studentGroup.SiteEntity.SiteId.ToString()!
        };

        // Assert

        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Capacity, result.Capacity);
        Assert.Equal(expected.PreferedSite, result.PreferedSite);
    }

    [Fact]
    public void Test_EquipmentAdapter()
    {
        // Arrange

        #region Mock Data

        var equipmentType = new EquipmentTypeEntity();
        equipmentType.EquipmentTypeId = new Guid();
        equipmentType.Name = "Retroprojecteur";
        equipmentType.Code = "Projo";
        equipmentType.Description = "Retroprojecteur";
        equipmentType.CreatedAt = DateTime.Now;
        equipmentType.UpdatedAt = DateTime.Now;

        var equipment = new EquipmentEntity();
        equipment.EquipmentId = new Guid();
        equipment.EquipmentTypeEntity = equipmentType;
        equipment.EquipmentTypeId = equipmentType.EquipmentTypeId;
        equipment.Name = "Retroprojecteur Epson EB-2255U";
        equipment.Code = "Projo-EB-2255U";
        equipment.Brand = "Epson";
        equipment.Model = "EB-2255U";
        equipment.Description = "Retroprojecteur Epson EB-2255U";
        equipment.CreatedAt = DateTime.Now;
        equipment.UpdatedAt = DateTime.Now;

        var site = new SiteEntity();
        site.Name = "Site A";
        site.Code = "A";

        var universitySiteEquipment = new UniversitySiteEquipmentEntity();
        universitySiteEquipment.SiteEntity = site;
        universitySiteEquipment.EquipmentEntity = equipment;

        equipment.UniversitySiteEquipmentEntity = universitySiteEquipment;

        #endregion

        // Act

        var result = EquipmentAdapter.Adapt(equipment);

        var expected = new Equipement(
            universitySiteEquipment.SiteEntity.SiteId.ToString()!,
            equipment.EquipmentTypeEntity.EquipmentTypeId.ToString()!,
            equipment.EquipmentId
        );

        // Assert

        Assert.Equal(expected.Site, result.Site);
        Assert.Equal(expected.Type, result.Type);
        Assert.Equal(expected.Code, result.Code);
    }

    [Fact]
    public void Test_CourseGroupsAdapter()
    {
        // Arrange

        #region Moc

        var course = new CourseEntity();
        course.Name = "Mathématiques";
        course.Code = "MATH";
        course.Description = "Mathématiques";
        course.TotalHours = 60;

        var studentGroup = new StudentGroupEntity();
        studentGroup.Code = "G1";
        studentGroup.NumberOfStudents = 30;
        studentGroup.SiteEntity = new SiteEntity() { Code = "A", Name = "Site A" };

        var equipmentType = new EquipmentTypeEntity();
        equipmentType.Name = "Retroprojecteur";
        equipmentType.Code = "Projo";

        course.EquipmentTypes = new()
        {
            new CourseEquipmentTypeEntity() { EquipmentTypeEntity = equipmentType }
        };

        #endregion

        // Act

        var result = CourseGroupsAdapter.Adapt(course, new List<StudentGroupEntity> { studentGroup });

        var expected = new CourseGroups
        {
            Course = course.CourseId.ToString()!,
            Groups = new List<Group>
            {
                new Group
                {
                    Name = studentGroup.Code,
                    Capacity = studentGroup.NumberOfStudents,
                    PreferedSite = studentGroup.SiteEntity.Code
                }
            },
            Equipements = new List<Equipement>
            {
                new Equipement(null, equipmentType.Name, null)
            }
        };

        // Assert

        Assert.Equal(expected.Course, result.Course);
        Assert.Equal(expected.Groups.Count, result.Groups.Count);
        Assert.Equal(expected.Equipements!.Count, result.Equipements!.Count);
    }
}