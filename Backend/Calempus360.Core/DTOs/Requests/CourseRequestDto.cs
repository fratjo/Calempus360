using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Requests
{
    public class CourseRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalHours { get; set; }
        public int WeeklyHours {  get; set; }
        public string Semester { get; set; } = string.Empty; 
        public int Credits { get; set; }
        public Dictionary<Guid,int>? EquipmentType { get; set; } = new Dictionary<Guid,int>();
    }

    public class CourseRequestDtoValidator : AbstractValidator<CourseRequestDto>
    {
        public CourseRequestDtoValidator() 
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Course Name is required !");
            RuleFor(c => c.Code).NotEmpty().WithMessage("Course Code is required !");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Course Description is required !");
            RuleFor(c => c.Semester).NotEmpty().WithMessage("Course Semester is required !");
            RuleFor(c => c.TotalHours).GreaterThan(0).WithMessage("Course Total Hours must be greater than 0 !");
            RuleFor(c => c.WeeklyHours).GreaterThan(0).WithMessage("Course Weekly Hour must be greater than 0 !");
            RuleFor(c => c.Credits).GreaterThan(0).WithMessage("Course Credits must be greater than 0 !");
            RuleForEach(c => c.EquipmentType).Must(et => et.Value > 0).WithMessage("All Equipment quantities must be greater than 0 !");
        }
    }
}
