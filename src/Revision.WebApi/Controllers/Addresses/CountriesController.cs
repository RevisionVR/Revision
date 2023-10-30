using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Addresses;

namespace Revision.WebApi.Controllers.Addresses;

public class CountriesController : BaseController
{
    private readonly ICountryService _countryService;
    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpPost("set")]
    public async Task<IActionResult> PostAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _countryService.SetAsync()
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _countryService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _countryService.GetAllAsync()
        });
}