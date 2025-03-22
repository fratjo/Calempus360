using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Responses
{
    public class SessionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public ClassroomResponseDto? Classroom { get; set; }
        public CourseResponseDto? Course { get; set; }
        public List<StudentGroupResponseDto>? StudentGroups { get; set; }
        public List<EquipmentResponseDto>? Equipments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }

    public static partial class DtoMapper
    {
        public static SessionResponseDto MapToDto(this Models.Session session)
        {
            return new SessionResponseDto
            {
                Id = session.Id,
                Name = session.Name,
                DateTimeStart = session.DateTimeStart,
                DateTimeEnd = session.DateTimeEnd,
                CreatedAt = session.CreatedAt,
                UpdatedAt = session.UpdatedAt,
                Equipments = session.Equipments?.Select(e => e.MapToDto()).ToList(),
                StudentGroups = session.StudentGroups?.Select(sg => sg.MapToDto()).ToList(),
                Course = session.Course?.MapToDto(),
                Classroom = session.Classroom?.MapToDto(),
            };
        }
    }
}
