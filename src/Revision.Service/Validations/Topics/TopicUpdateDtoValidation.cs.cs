using FluentValidation;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.Validations;

public class TopicUpdateDtoValidation : AbstractValidator<TopicUpdateDto>
{
    public TopicUpdateDtoValidation()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().MinimumLength(3)
            .WithMessage("Topic name minimum 3 characters").MaximumLength(50)
                .WithMessage("Topic name maximums length 50 characters");

        RuleFor(dto => dto.Price).NotEmpty().NotNull().WithMessage("This is require");

        RuleFor(dto => dto.SubjectId).NotEmpty().NotEmpty().WithMessage("This is require");
    }
}
