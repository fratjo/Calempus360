using FluentValidation;
using Microsoft.AspNetCore.Routing;

namespace Calempus360.Core.DTOs.Requests;

public class ClassroomRequestDto
{
    public string Name     { get; set; } = string.Empty;
    public string Code     { get; set; } = string.Empty;
    public int    Capacity { get; set; }
}

public class ClassroomRequestDtoValidator : AbstractValidator<ClassroomRequestDto>
{
    public ClassroomRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Classroom Name is required");
        RuleFor(x => x.Code).NotEmpty().WithMessage("Classroom Code is required");
        RuleFor(x => x.Capacity).NotEmpty().WithMessage("Classroom Capacity is required");
        RuleFor(x => x.Capacity).GreaterThanOrEqualTo(20).WithMessage("Classroom Capacity must be greater than 20");
    }
}