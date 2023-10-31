using FluentValidation;
using Revision.Service.DTOs.TopicPayments;

namespace Revision.Service.Validations.Payments.Topics;

public class TopicPaymentCreateDtoValidation : AbstractValidator<TopicPaymentCreationDto>
{
    public TopicPaymentCreateDtoValidation()
    {
        RuleFor(dto => dto.Price).NotEmpty();

        RuleFor(dto => dto.EducationId).NotEmpty();
    }
}
