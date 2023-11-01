using FluentValidation;
using Revision.Service.DTOs.Devices;

namespace Revision.Service.Validations.Devices;

public class DeviceUpdateDtoValidator : AbstractValidator<DeviceUpdateDto>
{
    public DeviceUpdateDtoValidator()
    {
        RuleFor(dto => dto.UniqueId)
            .NotNull()
            .NotEmpty();

        RuleFor(dto => dto.Price)
            .NotNull()
            .NotEmpty();
    }
}