using FluentValidation;
using Revision.Service.DTOs.Devices;

namespace Revision.Service.Validations.Devices;

public class DeviceUpdateDtoValidation : AbstractValidator<DeviceUpdateDto>
{
    public DeviceUpdateDtoValidation()
    {
        RuleFor(dto => dto.Price).NotNull().NotEmpty();
    }
}
