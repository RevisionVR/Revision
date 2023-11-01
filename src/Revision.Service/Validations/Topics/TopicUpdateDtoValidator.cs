using FluentValidation;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.Validations.Topics;

public class TopicUpdateDtoValidator : AbstractValidator<TopicUpdateDto>
{
    public TopicUpdateDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Topic name minimum 3 characters")
            .MaximumLength(50)
            .WithMessage("Topic name maximums length 50 characters");

        RuleFor(dto => dto.Price)
            .NotNull()
            .NotEmpty()
            .WithMessage("This is require");

        RuleFor(dto => dto.SubjectId)
            .NotNull()
            .NotEmpty()
            .WithMessage("This is require");
    }
}
