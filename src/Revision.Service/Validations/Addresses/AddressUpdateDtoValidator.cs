using FluentValidation;
using Revision.Service.DTOs.Addresses;

namespace Revision.Service.Validations.Addresses;

public class AddressUpdateDtoValidator : AbstractValidator<AddressUpdateDto>
{
    public AddressUpdateDtoValidator()
    {
        RuleFor(dto => dto.Home)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Home minimum characters 3")
            .MaximumLength(200)
            .WithMessage("Home maximums characters 200");

        RuleFor(dto => dto.CountryId)
            .NotNull()
            .NotEmpty();

        RuleFor(dto => dto.RegionId)
            .NotNull()
            .NotEmpty();

        RuleFor(dto => dto.DistrictId)
            .NotNull()
            .NotEmpty();
    }
}