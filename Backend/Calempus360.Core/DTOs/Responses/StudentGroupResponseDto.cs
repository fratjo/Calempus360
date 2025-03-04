using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Responses
{
    public class StudentGroupResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code {  get; set; } = string.Empty;
        public string OptionGrade {  get; set; } = string.Empty;
        public string Option {  get; set; } = string.Empty;
        public int NumberOfStudents { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public static partial class DtoMapper
    {
        public static StudentGroupResponseDto MapToDto(this StudentGroup studentGroup)
        {
            return new StudentGroupResponseDto
            {
                Id = studentGroup.Id,
                Name = studentGroup.Na,
                Code = studentGroup.Code,
                OptionGrade = studentGroup.OptionGrade.ToString(),
                Option = studentGroup.Option.Name,
                CreatedAt = studentGroup.CreatedAt,
                UpdatedAt = studentGroup.UpdatedAt,
                NumberOfStudents = studentGroup.NumberOfStudents
                Equipments = university.Equipments?.Select(equipment => equipment.MapToDTO()).ToList()
            };
        }
    }
}
