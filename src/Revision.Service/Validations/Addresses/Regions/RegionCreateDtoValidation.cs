using FluentValidation;
using Revision.Service.DTOs.Regions;

namespace Revision.Service.Validations.Addresses.Regions;

public class RegionCreateDtoValidation : AbstractValidator<RegionCreationDto>
{
    public RegionCreateDtoValidation()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotEmpty().MinimumLength(3)
            .WithMessage("Country must be more than 3 characters")
            .MaximumLength(30).WithMessage("Category must be more than 30 characters");

        RuleFor(dto => dto.CountryId).NotEmpty().NotNull();
    }
}
