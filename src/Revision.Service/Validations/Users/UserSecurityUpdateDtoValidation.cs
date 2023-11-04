using FluentValidation;
using Revision.Service.DTOs.Users;

namespace Revision.Service.Validations.Users;

public class UserSecurityUpdateDtoValidation : AbstractValidator<UserSecurityUpdateDto>
{
    public UserSecurityUpdateDtoValidation()
    {
        RuleFor(dto => dto.OldPassword).NotEmpty().WithMessage("It is require");

        RuleFor(dto => dto.NewPassword).NotEmpty().WithMessage("It is require")
            .Must(newPassword => PasswordValidation.IsStrongPassword(newPassword).IsValid)
                .WithMessage("Password  not stronger");

        RuleFor(dto => dto.ReturnNewPassword).NotEmpty().WithMessage("It is require")
           .Must(newPassword => PasswordValidation.IsStrongPassword(newPassword).IsValid)
               .WithMessage("Password  not stronger");
    }
}
