using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Devices;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Devices;

public class CommonDeviceCountController : BaseController
{
    private readonly IDeviceCountService _deviceCountService;
    public CommonDeviceCountController(IDeviceCountService deviceCountService)
    {
        _deviceCountService = deviceCountService;
    }

    [HttpGet("get-count-by-education/{educationId:long}")]
    public async Task<IActionResult> GetCountByEducactionIdAsync(long educationId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceCountService.GetCountByEducactionIdAsync(educationId)
        });
}