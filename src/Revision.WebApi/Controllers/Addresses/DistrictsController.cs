using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Addresses;
using Revision.WebApi.Controllers.Common;

namespace Revision.WebApi.Controllers.Addresses;

public class DistrictsController : BaseController
{
    private readonly IDistrictService _districtService;
    public DistrictsController(IDistrictService districtService)
    {
        _districtService = districtService;
    }

    [HttpPost("set")]
    public async Task<IActionResult> PostAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _districtService.SetAsync()
        });


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