using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Responses
{
    public class CourseResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalHours { get; set; }
        public int WeeklyHours { get; set; }
        public string Semester { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string EquipmentType {  get; set; } = string.Empty; //TODO : Remplacer par DTO equipmentType
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public static partial class DtoMapper
    {
        public static CourseResponseDto MapToDto(this Models.Course course)
        {
            return new CourseResponseDto
            {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code,
                Description = course.Description,
                TotalHours = course.TotalHours,
                WeeklyHours = course.WeeklyHours,
                Semester = course.Semester,
                Credits = course.Credits,
                EquipmentType = "",//TODO : Remplacer quand on a le DTO
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,

            };
        }
    }
}
