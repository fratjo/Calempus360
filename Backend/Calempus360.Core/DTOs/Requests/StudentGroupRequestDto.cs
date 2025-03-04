using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Requests
{
    public class StudentGroupRequestDto
    {
        public string Code { get; set; } = string.Empty;
        public int OptionGrade { get; set; }
        public Guid Option { get; set; }
        public int NumberOfStudents { get; set; }
        public Guid Site { get; set; }
    }

    public class StudentGroupRequestDtoValidator : AbstractValidator<StudentGroupRequestDto>
    {
        public StudentGroupRequestDtoValidator()
        {
            RuleFor(sg => sg.Code).NotEmpty().WithMessage("Student Group Code is required !");
            RuleFor(sg => sg.OptionGrade).GreaterThan(0).WithMessage("Option Grade must be greater than 0 !");
            RuleFor(sg => sg.Option).NotEmpty().WithMessage("Student Group Option is required !");
            RuleFor(sg => sg.NumberOfStudents).InclusiveBetween(20, 40).WithMessage("Student Group Size must be between 20 and 40 included !");
            RuleFor(sg => sg.Site).NotEmpty().WithMessage("Student Group Site is required !");
        }
    }
}
