using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calempus360.API.Controllers;
using Calempus360.Core.DTOs.Requests;
using Calempus360.Core.Models;
using Calempus360.Core.Interfaces.University;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class UniversityControllerTests
{
    private readonly Mock<IUniversityService> universityServiceMock;
    private readonly UniversityController controller;

    public UniversityControllerTests()
    {
        universityServiceMock = new Mock<IUniversityService>();
        controller = new UniversityController(universityServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfUniversities()
    {
        // Arrange
        var universities = new List<University>
        {
            new University("Uni1", "Code1", "Phone1", "Address1"),
            new University("Uni2", "Code2", "Phone2", "Address2")
        };
        universityServiceMock.Setup(s => s.GetAllAsync())
            .ReturnsAsync(universities);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedList = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
        Assert.Equal(universities.Count, returnedList.Count());
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WithUniversity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var university = new University("UniTest", "CodeTest", "Phone", "Address");
        universityServiceMock.Setup(s => s.GetByIdAsync(id))
            .ReturnsAsync(university);

        // Act
        var result = await controller.GetById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        // Ici, on peut vérifier le mapping en DTO si nécessaire
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task Add_ReturnsCreatedAtActionResult_WithUniversityDto()
    {
        // Arrange
        var requestDto = new UniversityRequestDto
        {
            Name = "UniAdd",
            Code = "CodeAdd",
            Phone = "PhoneAdd",
            Address = "AddressAdd"
        };
        var createdUniversity = new University("UniAdd", "CodeAdd", "PhoneAdd", "AddressAdd");
        universityServiceMock.Setup(s => s.PostNewUniversityAsync(It.IsAny<University>()))
            .ReturnsAsync(createdUniversity);

        // Act
        var result = await controller.Add(requestDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.NotNull(createdResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WithUpdatedUniversityDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var requestDto = new UniversityRequestDto
        {
            Name = "UniUpdate",
            Code = "CodeUpdate",
            Phone = "PhoneUpdate",
            Address = "AddressUpdate"
        };
        var updatedUniversity = new University("UniUpdate", "CodeUpdate", "PhoneUpdate", "AddressUpdate", id);
        universityServiceMock.Setup(s => s.UpdateUniversityAsync(It.IsAny<University>()))
            .ReturnsAsync(updatedUniversity);

        // Act
        var result = await controller.Update(id, requestDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();
        universityServiceMock.Setup(s => s.DeleteUniversityAsync(id))
            .Returns(Task.FromResult(true));

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}