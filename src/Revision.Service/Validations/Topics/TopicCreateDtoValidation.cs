using FluentValidation;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.Validations.Topics;

public class TopicCreateDtoValidation : AbstractValidator<TopicCreationDto>
{
    public TopicCreateDtoValidation()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().MinimumLength(3)
            .WithMessage("Topic name minimum 3 characters").MaximumLength(50)
                .WithMessage("Topic name maximums length 50 characters");

        RuleFor(dto => dto.Price).NotEmpty().NotNull().WithMessage("This is require");

        RuleFor(dto => dto.SubjectId).NotEmpty().NotEmpty().WithMessage("This is require");
    }
}
