using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.Interfaces.Topics;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Topics;

public class CommonTopicsController : BaseController
{
    private readonly ITopicService _topicService;
    public CommonTopicsController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicService.GetAllAsync()
        });


    [HttpGet("get-all-by-page")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams pagination,
        [FromQuery] string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicService.GetAllAsync(pagination, search)
        });
}
