using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Devices;

namespace Revision.WebApi.Controllers.Common.Devices;

public class CommonDevicesController : BaseController
{
    private readonly IDeviceService _deviceService;
    public CommonDevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet("get-by-education/{educationId:long}")]
    public async Task<IActionResult> GetAsync(long educationId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.GetByEducationIdAsync(educationId)
        });
}