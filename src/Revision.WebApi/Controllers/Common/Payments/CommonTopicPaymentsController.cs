using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Payments;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Payments;

public class CommonTopicPaymentsController : BaseController
{
    private readonly ITopicPaymentService _topicPaymentService;
    public CommonTopicPaymentsController(ITopicPaymentService topicPaymentService)
    {
        _topicPaymentService = topicPaymentService;
    }

    [HttpGet("get-by-education/{educationId:long}")]
    public async Task<IActionResult> GeAsync(long educationId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicPaymentService.GetByEducationIdAsync(educationId)
        });
}