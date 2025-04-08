using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class SiteResponseDto
{
    public Guid?                    Id         { get; set; }
    public string?                  Name       { get; set; }
    public string?                  Code       { get; set; }
    public string?                  Address    { get; set; }
    public string?                  Phone      { get; set; }
    public DateTime?                CreatedAt  { get; set; }
    public DateTime?                UpdatedAt  { get; set; }
    public List<ClassroomResponseDto>? Classrooms { get; set; }
    public List<ScheduleResponseDto>?  Schedules  { get; set; }
    public List<EquipmentResponseDto>? Equipments { get; set; }
}

public static partial class DtoMapper
{
    public static SiteResponseDto MapToDto(this Site site)
    {
        return new SiteResponseDto 
        {
            Id         = site.Id,
            Name       = site.Name,
            Code       = site.Code,
            Address    = site.Address,
            Phone      = site.Phone,
            CreatedAt  = site.CreatedAt,
            UpdatedAt  = site.UpdatedAt,
            Classrooms = site.Classrooms?.Select(classroom => classroom.MapToDto()).ToList(),
            Schedules  = site.Schedules?.Select(schedule => schedule.MapToDto()).ToList(),
            Equipments = site.Equipments?.Select(equipment => equipment.MapToDto()).ToList()
        };
    }
}