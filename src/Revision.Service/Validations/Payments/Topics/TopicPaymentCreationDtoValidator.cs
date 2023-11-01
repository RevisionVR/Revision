using FluentValidation;
using Revision.Service.DTOs.TopicPayments;

namespace Revision.Service.Validations.Payments.Topics;

public class TopicPaymentCreationDtoValidator : AbstractValidator<TopicPaymentCreationDto>
{
    public TopicPaymentCreationDtoValidator()
    {
        RuleFor(dto => dto.Price).NotEmpty();

        RuleFor(dto => dto.EducationId).NotEmpty();
    }
}
