using FluentValidation;
using Revision.Service.DTOs.Users;

namespace Revision.Service.Validations.Users;

public class UserCreateDtoValidation : AbstractValidator<UserCreationDto>
{
    public UserCreateDtoValidation()
    {
        RuleFor(dto => dto.FirstName)
            .MinimumLength(3)
            .WithMessage("FirstName must be more than 3 characters")
            .MaximumLength(30)
            .WithMessage("FirstName must be less than 30 characters");

        RuleFor(dto => dto.LastName)
            .MinimumLength(3)
            .WithMessage("LastName must be more than 3 characters")
            .MaximumLength(30)
            .WithMessage("LastName must be less than 30 characters");

        RuleFor(dto => dto.Password)
            .Must(password => PasswordValidation.IsStrongPassword(password).IsValid)
            .WithMessage("Password not stronger");

        RuleFor(dto => dto.Phone)
            .Must(phone => PhoneValidation.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Email)
            .Must(email => EmailValidation.IsValid(email))
            .WithMessage("Email address contains '.' and '@'");
    }
}