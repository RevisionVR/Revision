using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Addresses;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Addresses;

public class CommonRegionsController : BaseController
{
    private readonly IRegionService _regionService;
    public CommonRegionsController(IRegionService regionService)
    {
        _regionService = regionService;
    }

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
