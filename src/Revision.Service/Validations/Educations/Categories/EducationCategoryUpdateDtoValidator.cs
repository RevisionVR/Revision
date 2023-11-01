using FluentValidation;
using Revision.Service.DTOs.EducationCategories;

namespace Revision.Service.Validations.Educations.Categories;

public class EducationCategoryUpdateDtoValidator : AbstractValidator<EducationCategoryUpdateDto>
{
    public EducationCategoryUpdateDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .WithMessage("Category name minimum 3 characters")
            .WithMessage("EducationCategoryCreationDto Name maximums length 100 characters");
    }
}
