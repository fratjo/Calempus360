using System.Text.Json.Serialization;
using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class UniversityResponseDto
{
    public Guid                     Id         { get; set; }
    public string                   Name       { get; set; } = string.Empty;
    public string                   Code       { get; set; } = string.Empty;
    public string                   Phone      { get; set; } = string.Empty;
    public string                   Address    { get; set; } = string.Empty;
    public DateTime                 CreatedAt  { get; set; }
    public DateTime                 UpdatedAt  { get; set; }
    public List<SiteResponseDto>?      Sites      { get; set; }
    public List<EquipmentResponseDto>? Equipments { get; set; }
}

public static partial class DtoMapper
{
    public static UniversityResponseDto MapToDto(this University university)
    {
        return new UniversityResponseDto
        {
            Id         = university.Id,
            Name       = university.Name,
            Code       = university.Code,
            Phone      = university.Phone,
            Address    = university.Address,
            CreatedAt  = university.CreatedAt,
            UpdatedAt  = university.UpdatedAt,
            Sites      = university.Sites?.Select(site => site.MapToDto()).ToList(),
            Equipments = university.Equipments?.Select(equipment => equipment.MapToDTO()).ToList()
        };
    }
}