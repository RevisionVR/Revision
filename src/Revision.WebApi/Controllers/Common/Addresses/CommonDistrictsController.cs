using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Addresses;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Addresses;

public class CommonDistrictsController : BaseController
{
    private readonly IDistrictService _districtService;
    public CommonDistrictsController(IDistrictService districtService)
    {
        _districtService = districtService;
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _districtService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _districtService.GetAllAsync()
        });
}