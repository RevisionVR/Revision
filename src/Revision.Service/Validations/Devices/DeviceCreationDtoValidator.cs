using FluentValidation;
using Revision.Service.DTOs.Devices;

namespace Revision.Service.Validations.Devices;

public class DeviceCreationDtoValidator : AbstractValidator<DeviceCreationDto>
{
    public DeviceCreationDtoValidator()
    {
        RuleFor(dto => dto.UniqueId)
            .NotNull()
            .NotEmpty();

        RuleFor(dto => dto.Price)
            .NotNull()
            .NotEmpty();
    }
}
