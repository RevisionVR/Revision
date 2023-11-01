using FluentValidation;
using Revision.Service.DTOs.Districts;

namespace Revision.Service.Validations.Addresses.Districts;

public class DistrictCreateDtoValidation : AbstractValidator<DistrictCreationDto>
{
    public DistrictCreateDtoValidation()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Country must be more than 3 characters")
            .MaximumLength(30).WithMessage("Category must be more than 30 characters");

        RuleFor(dto => dto.RegionId)
            .NotEmpty()
            .NotNull();
    }
}
