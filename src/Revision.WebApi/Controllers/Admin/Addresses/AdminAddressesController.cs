using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Countries;
using Revision.Service.DTOs.Districts;
using Revision.Service.DTOs.Regions;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Validations.Addresses.Countries;
using Revision.Service.Validations.Addresses.Districts;
using Revision.Service.Validations.Addresses.Regions;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Addresses;

public class AdminAddressesController : AdminBaseController
{
    private IRegionService _regionService;
    private ICountryService _countryService;
    private IDistrictService _districtService;
    public AdminAddressesController(
        IRegionService regionService,
        ICountryService countryService,
        IDistrictService districtService)
    {
        _regionService = regionService;
        _countryService = countryService;
        _districtService = districtService;
    }

    [HttpPost("country/create")]
    public async Task<IActionResult> PostContryAsync([FromForm] CountryCreationDto dto)
    {
        var validation = new CountryCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _countryService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPatch("country/set")]
    public async Task<IActionResult> SetContryAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _countryService.SetAsync()
        });


    [HttpPost("region/create")]
    public async Task<IActionResult> PostRegionAsync([FromForm] RegionCreationDto dto)
    {
        var validation = new RegionCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _regionService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPatch("region/set")]
    public async Task<IActionResult> SetRegionAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _regionService.SetAsync()
        });


    [HttpPost("district/create")]
    public async Task<IActionResult> PostDistrictAsync([FromForm] DistrictCreationDto dto)
    {
        var validation = new DistrictCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _districtService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPatch("district/set")]
    public async Task<IActionResult> SetDistrictAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _districtService.SetAsync()
        });
}