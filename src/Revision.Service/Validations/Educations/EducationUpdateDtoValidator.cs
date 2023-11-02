using FluentValidation;
using Revision.Service.DTOs.Educations;

namespace Revision.Service.Validations.Educations;

public class EducationUpdateDtoValidator : AbstractValidator<EducationUpdateDto>
{
    public EducationUpdateDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Category must be more than 3 characters");

        RuleFor(dto => dto.Phone)
            .Must(phone => PhoneValidation.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Email)
            .Must(email => EmailValidation.IsValid(email))
            .WithMessage("Email address contains '.' and '@'");

        RuleFor(dto => dto.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Description minimum length 3 characters")
            .MaximumLength(100)
            .WithMessage("Description maximums length 100 characters");

        RuleFor(dto => dto.EducationCategoryId)
            .NotNull()
            .NotEmpty();
    }
}