using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Requests
{
    public class DayWithoutCourseRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }

    public class DayWithoutCourseRequestDtoValidator : AbstractValidator<DayWithoutCourseRequestDto>
    {
        public DayWithoutCourseRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("A Name is required");

            RuleFor(x => x.Date).NotEmpty().WithMessage("A Date is required")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("The date must be today or in the future");

        }
    }
}
