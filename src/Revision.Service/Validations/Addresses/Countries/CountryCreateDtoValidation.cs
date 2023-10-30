using FluentValidation;
using Revision.Service.DTOs.Countries;

namespace Revision.Service.Validations.Addresses.Countries;

public class CountryCreateDtoValidation : AbstractValidator<CountryCreationDto>
{
    public CountryCreateDtoValidation()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotEmpty().MinimumLength(3)
            .WithMessage("Country must be more than 3 characters")
            .MaximumLength(30).WithMessage("Category must be more than 30 characters");

        RuleFor(dto => dto.CountryCode).NotEmpty().MinimumLength(1)
            .WithMessage("Category must be more than 1 characters")
            .MaximumLength(5).WithMessage("Category must be less than 5 characters");
    }
}
