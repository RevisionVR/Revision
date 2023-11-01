using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Countries;
using Revision.Service.DTOs.Districts;
using Revision.Service.DTOs.Regions;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Validations.Addresses.Countries;
using Revision.Service.Validations.Addresses.Districts;
using Revision.Service.Validations.Addresses.Regions;

namespace Revision.WebApi.Controllers.Admin.Addresses;

[Route("api/[controller]")]
[ApiController]
public class AdminAddressController : AdminBaseController
{
    private ICountryService _country;
    private IRegionService _region;
    private IDistrictService _district;

    public AdminAddressController(
        ICountryService countryService,
        IRegionService regionService,
        IDistrictService districtService)
    {
        this._country = countryService;
        this._region = regionService;
        this._district = districtService;
    }

    [HttpPost("country/post")]
    public async Task<IActionResult> CreateCountryAsync([FromForm] CountryCreationDto dto)
    {
        var validation = new CountryCreateDtoValidation();
        var isValid = validation.Validate(dto);

        if (isValid.IsValid)
            return Ok(await _country.SetAsync());

        return BadRequest();
    }

    [HttpPost("region/post")]
    public async Task<IActionResult> CreateRegionAsync([FromForm] RegionCreationDto dto)
    {
        var validation = new RegionCreateDtoValidation();
        var isValid = validation.Validate(dto);

        if (isValid.IsValid)
            return Ok(await _region.SetAsync());

        return BadRequest();
    }

    [HttpPost("district/post")]
    public async Task<IActionResult> CreateDistrictAsync([FromForm] DistrictCreationDto dto)
    {
        var validation = new DistrictCreateDtoValidation();
        var isValid = validation.Validate(dto);

        if (isValid.IsValid)
            return Ok(await _district.SetAsync());

        return BadRequest();
    }
}
