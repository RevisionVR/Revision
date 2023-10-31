using FluentValidation;
using Revision.Service.DTOs.SubjectCategories;

namespace Revision.Service.Validations.Subjects.Categories;

public class SubjectCategoryUpdateDtoValidation : AbstractValidator<SubjectCategoryUpdateDto>
{
    public SubjectCategoryUpdateDtoValidation()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().MinimumLength(3)
            .WithMessage("Subject category name minimum 3 characters").MaximumLength(50)
                .WithMessage("Subject category name maximums length 50 characters");
    }
}
