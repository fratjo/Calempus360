using Calempus360.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Responses
{
    public class OptionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Courses { get; set; } = new List<string>(); //Remplacer par DTO Courses au lieu de string
    }
    public static partial class DtoMapper
    {
        public static OptionResponseDto MapToDto(this Option option)
        {
            return new OptionResponseDto
            {
                Id = option.Id,
                Name = option.Name,
                Code = option.Code,
                Description = option.Description,
                Courses = new List<string>()
            };
        }
    }
}
