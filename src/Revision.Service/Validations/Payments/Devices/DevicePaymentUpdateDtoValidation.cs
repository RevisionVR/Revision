using FluentValidation;
using Revision.Service.DTOs.DevicePayments;

namespace Revision.Service.Validations.Payments.Devices;

public class DevicePaymentUpdateDtoValidation : AbstractValidator<DevicePaymentUpdateDto>
{
    public DevicePaymentUpdateDtoValidation()
    {
        RuleFor(dto => dto.Price).NotEmpty();

        RuleFor(dto => dto.EducationId).NotEmpty();
    }
}
