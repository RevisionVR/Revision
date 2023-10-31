using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Addresses;

namespace Revision.WebApi.Controllers.Common.Addresses;

public class RegionsController : BaseController
{
    private readonly IRegionService _regionService;
    public RegionsController(IRegionService regionService)
    {
        _regionService = regionService;
    }

    [HttpPost("set")]
    public async Task<IActionResult> PostAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _regionService.SetAsync()
        });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _regionService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _regionService.GetAllAsync()
        });
}
