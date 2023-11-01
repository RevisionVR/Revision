using FluentValidation;
using Revision.Service.DTOs.Addresses;

namespace Revision.Service.Validations.Addresses;

public class AddresseeCreationDtoValidation : AbstractValidator<AddressCreationDto>
{
    public AddresseeCreationDtoValidation()
    {
        RuleFor(dto => dto.Home).NotNull().NotEmpty().MinimumLength(10).WithMessage("Home Minimum characters 10 ")
            .MaximumLength(200).WithMessage("Home maximums characters 10");

        RuleFor(dto => dto.Description).NotEmpty().NotNull().MinimumLength(5)
            .WithMessage("Description minimum length 5 characters").MaximumLength(200)
            .WithMessage("Description maximums length 200 characters");

        RuleFor(dto => dto.CountryId).NotNull().NotEmpty();

        RuleFor(dto => dto.RegionId).NotNull().NotEmpty();

        RuleFor(dto => dto.DistrictId).NotNull().NotEmpty();

    }
}
