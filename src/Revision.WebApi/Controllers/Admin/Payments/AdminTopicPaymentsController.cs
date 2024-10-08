﻿using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.TopicPayments;
using Revision.Service.Interfaces.Payments;
using Revision.Service.Services.Payments;
using Revision.Service.Validations.Payments.Topics;
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
    {
        var validation = new TopicPaymentCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _paymentService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentService.DeleteAsync(id)
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GeAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentService.GetByIdAsync(id)
        });


    [HttpGet("get-all-by-page")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams pagination,
        [FromQuery] string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentService.GetAllAsync(pagination, search)
        });
}
