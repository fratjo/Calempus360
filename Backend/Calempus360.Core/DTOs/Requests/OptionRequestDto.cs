using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Requests
{
    public class OptionRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }

    public class OptionRequestDtoValidator : AbstractValidator<OptionRequestDto>
    {
        public OptionRequestDtoValidator()
        {
            RuleFor(o => o.Name).NotEmpty().WithMessage("Option Name is required !");
            RuleFor(o => o.Code).NotEmpty().WithMessage("Option Code is required !");
            RuleFor(o => o.Description).NotEmpty().WithMessage("Option Description is required !");
        }
    }
}
