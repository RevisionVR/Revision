using FluentValidation;
using Revision.Service.DTOs.Devices;

namespace Revision.Service.Validations.Devices;

public class DeviceCreateDtoValidation : AbstractValidator<DeviceCreationDto>
{
    public DeviceCreateDtoValidation()
    {
        RuleFor(dto => dto.UniqueId)
            .NotNull()
            .NotEmpty();

        RuleFor(dto => dto.Price)
            .NotNull()
            .NotEmpty();
    }
}
