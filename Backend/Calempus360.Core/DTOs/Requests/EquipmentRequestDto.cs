using FluentValidation;

namespace Calempus360.Core.DTOs.Requests;

public class EquipmentRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? EquipmentTypeId { get; set; }
    public Guid? ClassroomId { get; set; }
}

public class EquipmentRequestDtoValidator : AbstractValidator<EquipmentRequestDto>
{
    public EquipmentRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Brand).NotEmpty();
        RuleFor(x => x.Model).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}

public class EquipmentTypeRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class EquipmentTypeRequestDtoValidator : AbstractValidator<EquipmentTypeRequestDto>
{
    public EquipmentTypeRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}