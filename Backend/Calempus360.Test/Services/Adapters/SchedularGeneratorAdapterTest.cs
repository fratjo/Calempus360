using Calempus360.Core.Models;
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
        
        var classroom = new Classroom();
        classroom.ClassroomId = new Guid();
        classroom.Name = "Salle de cours A3";
        classroom.Code = "A3";
        classroom.Capacity = 50;
        classroom.CreatedAt = DateTime.Now;
        classroom.UpdatedAt = DateTime.Now;
        
        var site = new Site();
        site.SiteId = new Guid();
        site.Name = "Site A";
        site.Code = "A";
        site.CreatedAt = DateTime.Now;
        site.UpdatedAt = DateTime.Now;
        
        classroom.Site = site;
        
        var equipmentType = new EquipmentType();
        equipmentType.EquipmentTypeId = new Guid();
        equipmentType.Name = "Retroprojecteur";
        equipmentType.Code = "Projo";
        equipmentType.Description = "Retroprojecteur";
        equipmentType.CreatedAt = DateTime.Now;
        equipmentType.UpdatedAt = DateTime.Now;
        
        var equipment = new Equipment();
        equipment.EquipmentId = new Guid();
        equipment.EquipmentType = equipmentType;
        equipment.EquipmentTypeId = equipmentType.EquipmentTypeId;
        equipment.Name = "Retroprojecteur Epson EB-2255U";
        equipment.Code = "Projo-EB-2255U";
        equipment.Brand = "Epson";
        equipment.Model = "EB-2255U";
        equipment.Description = "Retroprojecteur Epson EB-2255U";
        equipment.CreatedAt = DateTime.Now;
        equipment.UpdatedAt = DateTime.Now;
        
        var classroomEquipment = new ClassroomEquipment();
        classroomEquipment.Equipment = equipment;
        classroomEquipment.EquipmentId = equipment.EquipmentId;
        classroomEquipment.Classroom = classroom;
        classroomEquipment.ClassroomId = classroom.ClassroomId;
        
        equipment.ClassroomEquipment = classroomEquipment;
        classroom.ClassroomEquipments = new (){ classroomEquipment };
        
        #endregion
        
        // Act
        
        var result = ClassAdapter.Adapt(classroom);
        
        var expected = new Class(
            classroom.Name,
            classroom.Site.Name,
            classroom.Capacity,
            new List<Equipement>
            {
                new Equipement(
                    classroom.Site.Name,
                    classroomEquipment.Equipment.EquipmentType.Name,
                    classroomEquipment.Equipment.EquipmentTypeId
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
        
        var studentGroup = new StudentGroup();
        studentGroup.Code = "G1";
        studentGroup.NumberOfStudents = 30;
        
        var site = new Site();
        site.SiteId = new Guid();
        site.Name   = "Site A";
        site.Code   = "A";
        
        studentGroup.Site = site;

        #endregion

        // Act
        
        var result = GroupAdapter.Adapt(studentGroup);
        
        var expected = new Group
        {
            Name = studentGroup.Code,
            Capacity = studentGroup.NumberOfStudents,
            PreferedSite = studentGroup.Site.Name
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

        var equipmentType = new EquipmentType();
        equipmentType.EquipmentTypeId = new Guid();
        equipmentType.Name            = "Retroprojecteur";
        equipmentType.Code            = "Projo";
        equipmentType.Description     = "Retroprojecteur";
        equipmentType.CreatedAt       = DateTime.Now;
        equipmentType.UpdatedAt       = DateTime.Now;
        
        var equipment = new Equipment();
        equipment.EquipmentId     = new Guid();
        equipment.EquipmentType   = equipmentType;
        equipment.EquipmentTypeId = equipmentType.EquipmentTypeId;
        equipment.Name            = "Retroprojecteur Epson EB-2255U";
        equipment.Code            = "Projo-EB-2255U";
        equipment.Brand           = "Epson";
        equipment.Model           = "EB-2255U";
        equipment.Description     = "Retroprojecteur Epson EB-2255U";
        equipment.CreatedAt       = DateTime.Now;
        equipment.UpdatedAt       = DateTime.Now;
        
        var site = new Site();
        site.Name = "Site A";
        site.Code = "A";
        
        var universitySiteEquipment = new UniversitySiteEquipment();
        universitySiteEquipment.Site = site;
        universitySiteEquipment.Equipment = equipment;
        
        equipment.UniversitySiteEquipment = universitySiteEquipment;

        #endregion

        // Act
        
        var result = EquipmentAdapter.Adapt(equipment);
        
        var expected = new Equipement(
            universitySiteEquipment.Site.Name,
            equipment.EquipmentType.Name,
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
        
        var course = new Course();
        course.Name = "Mathématiques";
        course.Code = "MATH";
        course.Description = "Mathématiques";
        course.TotalHours = 60;
        
        var studentGroup = new StudentGroup();
        studentGroup.Code             = "G1";
        studentGroup.NumberOfStudents = 30;
        studentGroup.Site             = new Site() { Code = "A", Name = "Site A" };
        
        var equipmentType = new EquipmentType();
        equipmentType.Name = "Retroprojecteur";
        equipmentType.Code = "Projo";
        
        course.EquipmentTypes = new ()
        {
            new CourseEquipmentType() { EquipmentType = equipmentType }
        };
        
        #endregion     
        
        // Act
        
        var result = CourseGroupsAdapter.Adapt(course, new List<StudentGroup> { studentGroup });
        
        var expected = new CourseGroups
        {
            Course = course.Name,
            Groups = new List<Group>
            {
                new Group
                {
                    Name = studentGroup.Code,
                    Capacity = studentGroup.NumberOfStudents,
                    PreferedSite = studentGroup.Site.Code
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