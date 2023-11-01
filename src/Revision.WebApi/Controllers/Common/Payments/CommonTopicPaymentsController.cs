using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Payments;

namespace Revision.WebApi.Controllers.Common.Payments;

public class CommonTopicPaymentsController : BaseController
{
    private readonly ITopicPaymentService _paymentService;
    public CommonTopicPaymentsController(ITopicPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("get-by-education/{educationId:long}")]
    public async Task<IActionResult> GeAsync(long educationId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _paymentService.GetByEducationIdAsync(educationId)
        });
}