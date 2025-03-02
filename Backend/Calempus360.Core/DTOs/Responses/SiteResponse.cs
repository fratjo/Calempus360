using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class SiteResponse
{
    public Guid?                    Id         { get; set; }
    public string?                  Name       { get; set; }
    public string?                  Code       { get; set; }
    public string?                  Address    { get; set; }
    public string?                  Phone      { get; set; }
    public DateTime?                CreatedAt  { get; set; }
    public DateTime?                UpdatedAt  { get; set; }
    public List<ClassroomResponse>? Classrooms { get; set; }
    public List<ScheduleResponse>?  Schedules  { get; set; }
    public List<EquipmentResponse>? Equipments { get; set; }
}

public static partial class DtoMapper
{
    public static SiteResponse MapToDto(this Site site)
    {
        return new SiteResponse 
        {
            Id         = site.Id,
            Name       = site.Name,
            Code       = site.Code,
            Address    = site.Address,
            Phone      = site.Phone,
            CreatedAt  = site.CreatedAt,
            UpdatedAt  = site.UpdatedAt,
            Classrooms = site.Classrooms?.Select(classroom => classroom.MapToDTO()).ToList(),
            Schedules  = site.Schedules?.Select(schedule => schedule.MapToDTO()).ToList(),
            Equipments = site.Equipments?.Select(equipment => equipment.MapToDTO()).ToList()
        };
    }
}