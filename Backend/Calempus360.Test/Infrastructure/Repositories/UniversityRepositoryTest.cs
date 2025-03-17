using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calempus360.Core.Models;
using Calempus360.Errors.CustomExceptions;
using Calempus360.Infrastructure.Data;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class UniversityRepositoryTests
{
    private Calempus360DbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<Calempus360DbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new Calempus360DbContext(options);
    }

    [Fact]
    public async Task GetUniversityByIdAsync_ReturnsUniversity_WhenUniversityExists()
    {
        // Arrange
        var dbContext = CreateInMemoryContext();
        var repository = new UniversityRepository(dbContext);
        var universityId = Guid.NewGuid();

        // On insère une entité de test dans le contexte
        var universityEntity = new UniversityEntity // Utilisez ici votre classe d'entité correspondante
        {
            UniversityId = universityId,
            Code = "TEST",
            Name = "Test University",
            Address = "Test Address",
            Phone = "1234567890",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Sites = new List<SiteEntity>()
        };
        await dbContext.Universities.AddAsync(universityEntity);
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetUniversityByIdAsync(universityId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test University", result.Name);
    }

    [Fact]
    public async Task GetUniversityByIdAsync_ThrowsNotFoundException_WhenUniversityDoesNotExist()
    {
        // Arrange
        var dbContext = CreateInMemoryContext();
        var repository = new UniversityRepository(dbContext);
        var nonExistingId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => repository.GetUniversityByIdAsync(nonExistingId));
    }
}