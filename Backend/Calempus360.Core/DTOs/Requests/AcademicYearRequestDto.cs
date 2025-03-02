using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FluentValidation;

namespace Calempus360.Core.DTOs.Requests;

public class AcademicYearRequestDto
{ 
    public string   Id        { get; set; } = string.Empty;
    public DateTime DateStart { get; set; }
    public DateTime DateEnd   { get; set; }
}

public class AcademicYearRequestDtoValidator : AbstractValidator<AcademicYearRequestDto>
{
    public AcademicYearRequestDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Academic Year Name is required");
        RuleFor(x => x.DateStart).NotEmpty().WithMessage("Academic Year Date Start is required");
        RuleFor(x => x.DateEnd).NotEmpty().WithMessage("Academic Year Date End is required");
        RuleFor(x => x.DateStart).LessThan(x => x.DateEnd).WithMessage("Start Date must be less than End Date");
    }
}