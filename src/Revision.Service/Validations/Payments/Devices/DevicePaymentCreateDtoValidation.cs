using FluentValidation;
using Revision.Service.DTOs.DevicePayments;

namespace Revision.Service.Validations.Payments.Devices;

public class DevicePaymentCreateDtoValidation : AbstractValidator<DevicePaymentCreationDto>
{
    public DevicePaymentCreateDtoValidation()
    {
        RuleFor(dto => dto.Price).NotEmpty();

        RuleFor(dto => dto.EducationId).NotEmpty();
    }
}
