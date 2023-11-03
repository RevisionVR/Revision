using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Payments;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Payments;

public class CommonDevicePaymentController : BaseController
{
    private readonly IDevicePaymentService _devicePaymentService;
    public CommonDevicePaymentController(IDevicePaymentService devicePaymentService)
    {
        _devicePaymentService = devicePaymentService;
    }

    [HttpGet("get-by-education/{educationId:long}")]
    public async Task<IActionResult> GeAsync(long educationId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _devicePaymentService.GetByEducationIdAsync(educationId)
        });
}