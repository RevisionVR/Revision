﻿using FluentValidation;
using Revision.Service.DTOs.Subjects;

namespace Revision.Service.Validations.Subjects;

public class SubjectCreationDtoValidator : AbstractValidator<SubjectCreationDto>
{
    public SubjectCreationDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Subject category name minimum 3 characters")
            .MaximumLength(50)
            .WithMessage("Subject category name maximums length 50 characters");

        RuleFor(dto => dto.SubjectCategoryId)
            .NotEmpty()
            .NotEmpty()
            .WithMessage("This is require");
    }
}