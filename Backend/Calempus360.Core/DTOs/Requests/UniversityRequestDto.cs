using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Calempus360.Core.DTOs.Requests;

public class UniversityRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class UniversityRequestDtoValidator : AbstractValidator<UniversityRequestDto>
{
    public UniversityRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("University Name is required");
        RuleFor(x => x.Code).NotEmpty().WithMessage("University Code is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("University Address is required");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("University City is required");
    }
}