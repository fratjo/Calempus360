using System.ComponentModel.DataAnnotations;
using Calempus360.Core.Models;
using FluentValidation;

namespace Calempus360.Core.DTOs.Requests;

public class SiteRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<Schedule> Schedules { get; set; } = new();
}

public class SiteRequestDtoValidator : AbstractValidator<SiteRequestDto>
{
    public SiteRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Site Name is required");
        RuleFor(x => x.Code).NotEmpty().WithMessage("Site Code is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Site Address is required");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Site Phone is required");
        RuleForEach(x => x.Schedules).SetValidator(new ScheduleValidator());
    }

    public class ScheduleValidator : AbstractValidator<Schedule>
    {
        public ScheduleValidator()
        {
            RuleFor(x => x.DayOfWeek).IsInEnum().WithMessage("Invalid Day of Week");
            RuleFor(x => x.TimeStart).NotEmpty().WithMessage("Time Start is required");
            RuleFor(x => x.TimeEnd).NotEmpty().WithMessage("Time End is required");
        }
    }
}