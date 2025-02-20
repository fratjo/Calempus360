using Calempus360.Core.Models;
using Calempus360.Infrastructure.Persistence.Entities;
using Calempus360.Infrastructure.Persistence.Mappers;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Calempus360.Test.Infrastructure.Persistence.Mappers;

public class ModelMappersTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ModelMappersTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void UniversityEntity_ToDomainModel()
    {
        // Arrange
        var universityEntity = new UniversityEntity
        {
            UniversityId = Guid.NewGuid(),
            Name         = "Université A",
            Code         = "UA123",
            Phone        = "123-456-7890",
            Address      = "123 Rue Université",
            CreatedAt    = DateTime.UtcNow,
            UpdatedAt    = DateTime.UtcNow,
            Sites = new List<SiteEntity>
            {
                new SiteEntity
                {
                    SiteId         = Guid.NewGuid(),
                    Name           = "Campus Principal",
                    Code           = "CP001",
                    Address        = "Rue A",
                    Phone          = "111-222-3333",
                    AcademicYearId = "2023-2024",
                    CreatedAt      = DateTime.UtcNow,
                    UpdatedAt      = DateTime.UtcNow
                }
            }
        };

        // Act
        var university = universityEntity.ToDomainModel();

        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(university));

        // Assert
        Assert.NotNull(university);
        Assert.Equal(universityEntity.Name, university.Name);
        Assert.NotNull(university.Sites);
        Assert.Contains("2023-2024", university.Sites.Keys);
        Assert.Single(university.Sites["2023-2024"]);
    }
    
    
    [Fact]
    public void University_ToEntity()
    {
        // Arrange
        var university = new University(
            id: Guid.NewGuid(),
            name: "Université A",
            code: "UA123",
            phone: "123-456-7890",
            address: "123 Rue Université",
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow,
            sites: new Dictionary<string, List<Site>>
            {
                {
                    "2023-2024",
                    new List<Site>
                    {
                        new Site(
                            id: Guid.NewGuid(),
                            name: "Campus Principal",
                            code: "CP001",
                            address: "Rue A",
                            phone: "111-222-3333",
                            createdAt: DateTime.UtcNow,
                            updatedAt: DateTime.UtcNow,
                            university: null,
                            classrooms: null,
                            schedules: null,
                            equipments: null,
                            studentGroups: null
                        )
                    }
                }
            },
            equipments : new Dictionary<string, List<Equipment>>
            {
                {
                    "2023-2024",
                    new List<Equipment>
                    {
                        new Equipment(
                            id: Guid.NewGuid(),
                            name: "Ordinateur de bureau",
                            code: "OD001",
                            brand: "Dell",
                            model: "Optiplex 9020",
                            description: "Ordinateur de bureau Dell Optiplex 9020",
                            createdAt: DateTime.UtcNow,
                            updatedAt: DateTime.UtcNow,
                            equipmentType: null,
                            university: null,
                            site: null,
                            classroom: null,
                            sessions: null
                        )
                    }
                }
            }
        );

        // Act
        var universityEntity = university.ToEntity();
        
        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(universityEntity));
        
        // Assert
        Assert.NotNull(universityEntity);
        Assert.Equal(university.Name, universityEntity.Name);
    }
}