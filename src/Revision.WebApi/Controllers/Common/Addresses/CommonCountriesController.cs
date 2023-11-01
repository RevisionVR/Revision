using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Addresses;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Addresses;

public class CommonCountriesController : BaseController
{
    private readonly ICountryService _countryService;
    public CommonCountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

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