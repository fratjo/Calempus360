using Calempus360.Core.Models;

namespace Calempus360.Core.DTOs.Responses;

public class ClassroomResponseDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int? Capacity { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<EquipmentResponseDto>? Equipments { get; set; }
    public Guid? Site { get; set; }
}

public static partial class DtoMapper
{
    public static ClassroomResponseDto MapToDto(this Classroom classroom)
    {
        return new ClassroomResponseDto
        {
            Id = classroom.Id,
            Name = classroom.Name,
            Code = classroom.Code,
            Capacity = classroom.Capacity,
            CreatedAt = classroom.CreatedAt,
            UpdatedAt = classroom.UpdatedAt,
            Equipments = classroom.Equipments?.Select(equipment => equipment.MapToDto()).ToList(),
            Site = classroom.Site
        };
    }
}