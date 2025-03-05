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
}

public class SiteRequestDtoValidator : AbstractValidator<SiteRequestDto>
{
    public SiteRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Site Name is required");
        RuleFor(x => x.Code).NotEmpty().WithMessage("Site Code is required");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Site Address is required");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Site Phone is required");
    }
}