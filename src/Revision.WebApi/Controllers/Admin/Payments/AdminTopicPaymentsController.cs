using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.TopicPayments;
using Revision.Service.Interfaces.Payments;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Payments;

public class AdminTopicPaymentsController : AdminBaseController
{
    private readonly ITopicPaymentService _paymentService;
    public AdminTopicPaymentsController(ITopicPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] TopicPaymentCreationDto dto)
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await _paymentService.CreateAsync(dto)
    });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GeAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentService.GetAllAsync(pagination)
        });
}
