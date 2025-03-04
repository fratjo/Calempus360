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
        public Guid Id { get; set; }
        public string Code {  get; set; } = string.Empty;
        public int OptionGrade {  get; set; }
        public OptionResponseDto? Option {  get; set; }
        public int NumberOfStudents { get; set; }
        public SiteResponseDto? Site { get; set; }
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
                Code = studentGroup.Code,
                OptionGrade = studentGroup.OptionGrade,
                Option = studentGroup.Option?.MapToDto(),
                CreatedAt = studentGroup.CreatedAt,
                UpdatedAt = studentGroup.UpdatedAt,
                NumberOfStudents = studentGroup.NumberOfStudents,
                Site = studentGroup.Site?.MapToDto()
            };
        }
    }
}
