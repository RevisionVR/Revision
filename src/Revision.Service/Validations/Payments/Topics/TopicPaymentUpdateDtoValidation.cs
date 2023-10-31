using FluentValidation;
using Revision.Service.DTOs.TopicPayments;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.Validations.Payments.Topics;

public class TopicPaymentUpdateDtoValidation : AbstractValidator<TopicPaymentUpdateDto>
{
    public TopicPaymentUpdateDtoValidation()
    {
        RuleFor(dto => dto.Price).NotEmpty();

        RuleFor(dto => dto.EducationId).NotEmpty();
    }
}
