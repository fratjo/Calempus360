using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calempus360.Core.Interfaces.Site;
using Calempus360.Core.Interfaces.University;
using Calempus360.Core.Models;
using Calempus360.Services.Services;
using Moq;
using Xunit;

public class UniversityServiceTests
{
    private readonly Mock<IUniversityRepository> universityRepoMock;
    private readonly Mock<ISiteService> siteServiceMock;
    private readonly UniversityService service;

    public UniversityServiceTests()
    {
        universityRepoMock = new Mock<IUniversityRepository>();
        siteServiceMock = new Mock<ISiteService>();
        service = new UniversityService(universityRepoMock.Object, siteServiceMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_Returns_University_When_Found()
    {
        // Arrange
        var universityId = Guid.NewGuid();
        var expectedUniversity = new University
        (
            name: "New University",
            code: "NEW",
            address: "New Address",
            phone: "1234567890"
        );

        universityRepoMock
            .Setup(r => r.GetUniversityByIdAsync(universityId))
            .ReturnsAsync(expectedUniversity);

        // Act
        var result = await service.GetByIdAsync(universityId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUniversity.Name, result.Name);
    }

    [Fact]
    public async Task PostNewUniversityAsync_Returns_University()
    {
        // Arrange
        var newUniversity = new University
        (
            name: "New University",
            code: "NEW",
            address: "New Address",
            phone: "1234567890"
        );


        universityRepoMock
            .Setup(r => r.PostNewUniversityAsync(newUniversity))
            .ReturnsAsync(newUniversity);

        // Act
        var result = await service.PostNewUniversityAsync(newUniversity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newUniversity.Name, result.Name);
    }

    [Fact]
    public async Task DeleteUniversityAsync_Returns_True_When_SiteDelete_Succeeds()
    {
        // Arrange
        var universityId = Guid.NewGuid();
        siteServiceMock
            .Setup(s => s.DeleteSiteByUniversityAsync(universityId))
            .ReturnsAsync(true);
        universityRepoMock
            .Setup(r => r.DeleteUniversityAsync(universityId))
            .Returns(Task.FromResult(true));

        // Act
        var result = await service.DeleteUniversityAsync(universityId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteUniversityAsync_Throws_InvalidOperationException_When_SiteDelete_Fails()
    {
        // Arrange
        var universityId = Guid.NewGuid();
        siteServiceMock
            .Setup(s => s.DeleteSiteByUniversityAsync(universityId))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await service.DeleteUniversityAsync(universityId);
        });
    }
}