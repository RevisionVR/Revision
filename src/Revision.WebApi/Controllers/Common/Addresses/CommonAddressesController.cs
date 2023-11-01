using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Addresses;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Addresses;

public class CommonAddressesController : BaseController
{
    private readonly IRegionService _regionService;
    private readonly ICountryService _countryService;
    private readonly IDistrictService _districtService;
    public CommonAddressesController(
        IRegionService regionService,
        ICountryService countryService,
        IDistrictService districtService)
    {
        _regionService = regionService;
        _countryService = countryService;
        _districtService = districtService;
    }

    [HttpGet("country/get/{id:long}")]
    public async Task<IActionResult> GetCountryByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _countryService.GetByIdAsync(id)
        });


    [HttpGet("country/get-all")]
    public async Task<IActionResult> GetCountryAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _countryService.GetAllAsync()
        });


    [HttpGet("district/get/{id:long}")]
    public async Task<IActionResult> GetDistrictByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _districtService.GetByIdAsync(id)
        });


    [HttpGet("district/get-all")]
    public async Task<IActionResult> GetDistrictAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _districtService.GetAllAsync()
        });


    [HttpGet("region/get/{id:long}")]
    public async Task<IActionResult> GetRegionByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _regionService.GetByIdAsync(id)
        });


    [HttpGet("region/get-all")]
    public async Task<IActionResult> GetRegionAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _regionService.GetAllAsync()
        });
}