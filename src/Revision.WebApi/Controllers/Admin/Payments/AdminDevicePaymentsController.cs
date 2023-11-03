using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.DevicePayments;
using Revision.Service.DTOs.TopicPayments;
using Revision.Service.Interfaces.Payments;
using Revision.Service.Validations.Payments.Devices;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Payments;

public class AdminDevicePaymentsController : AdminBaseController
{
    private readonly IDevicePaymentService _devicePaymentService;
    public AdminDevicePaymentsController(IDevicePaymentService devicePaymentService)
    {
        _devicePaymentService = devicePaymentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] DevicePaymentCreationDto dto)
    {
        var validation = new DevicePaymentCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _devicePaymentService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GeAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _devicePaymentService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _devicePaymentService.GetAllAsync(pagination)
        });
}