using FluentValidation;
using Revision.Service.DTOs.DevicePayments;

namespace Revision.Service.Validations.Payments.Devices;

public class DevicePaymentCreationDtoValidator : AbstractValidator<DevicePaymentCreationDto>
{
    public DevicePaymentCreationDtoValidator()
    {
        RuleFor(dto => dto.EducationId)
            .NotNull()
            .NotEmpty();
    }
}
