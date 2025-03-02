using System.Text.Json.Serialization;
using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class UniversityResponse
{
    public Guid                     Id         { get; set; }
    public string                   Name       { get; set; } = string.Empty;
    public string                   Code       { get; set; } = string.Empty;
    public string                   Phone      { get; set; } = string.Empty;
    public string                   Address    { get; set; } = string.Empty;
    public DateTime                 CreatedAt  { get; set; }
    public DateTime                 UpdatedAt  { get; set; }
    public List<SiteResponse>?      Sites      { get; set; }
    public List<EquipmentResponse>? Equipments { get; set; }
}

public static partial class DtoMapper
{
    public static UniversityResponse MapToDto(this University university)
    {
        return new UniversityResponse
        {
            Id         = university.Id ?? Guid.Empty,
            Name       = university.Name,
            Code       = university.Code,
            Phone      = university.Phone,
            Address    = university.Address,
            CreatedAt  = university.CreatedAt ?? DateTime.MinValue,
            UpdatedAt  = university.UpdatedAt ?? DateTime.MinValue,
            Sites      = university.Sites?.Select(site => site.MapToDto()).ToList(),
            Equipments = university.Equipments?.Select(equipment => equipment.MapToDTO()).ToList()
        };
    }
}