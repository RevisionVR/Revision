using FluentValidation;
using Revision.Service.DTOs.EducationCategories;

namespace Revision.Service.Validations.Educations.Categories;

public class EducationCategoryCreationDtoValidator : AbstractValidator<EducationCategoryCreationDto>
{
    public EducationCategoryCreationDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Category name minimum 3 characters")
            .WithMessage("Categoty Name maximums length 100 characters");
    }
}
