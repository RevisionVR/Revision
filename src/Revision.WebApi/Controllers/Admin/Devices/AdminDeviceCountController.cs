using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.Interfaces.Devices;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Devices;

public class AdminDeviceCountController : AdminBaseController
{
    private readonly IDeviceCountService _deviceCountService;
    public AdminDeviceCountController(IDeviceCountService deviceCountService)
    {
        _deviceCountService = deviceCountService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetCountAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceCountService.GetCountAllAsync()
        });

    [HttpGet("get-all-by-page")]
    public async Task<IActionResult> GetCountAllAsync([FromQuery] PaginationParams pagination, [FromQuery] string search = null)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceCountService.GetCountAllAsync(pagination, search)
        });
}