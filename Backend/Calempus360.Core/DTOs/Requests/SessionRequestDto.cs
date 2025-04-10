﻿using Calempus360.Core.DTOs.Responses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calempus360.Core.DTOs.Requests
{
    public class SessionRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public Guid Classroom { get; set; }
        public Guid Course { get; set; }
        public List<Guid> StudentGroups { get; set; } = new List<Guid>();
        public List<Guid> Equipments { get; set; } = new List<Guid>();
    }

    public class SessionRequestDtoValidator : AbstractValidator<SessionRequestDto>
    {
        public SessionRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name must be not empty !");
            RuleFor(x => x.StudentGroups).NotEmpty().WithMessage("At least one student group must be provided.");

            RuleFor(x => x.Classroom).NotNull().WithMessage("Classroom ID is required.");

            RuleFor(x => x.Course).NotNull().WithMessage("Course ID is required.");

            RuleFor(x => x.DateTimeStart).LessThan(x => x.DateTimeEnd).WithMessage("Start date must be before end date.");

            RuleFor(x => x.DateTimeStart).Must((dto, startDate) => startDate.AddHours(1) == dto.DateTimeEnd).WithMessage("Session duration must be 1 hour.");
        }
    }
}
