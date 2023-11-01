using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Topics;
using Revision.Service.Interfaces.Topics;

namespace Revision.WebApi.Controllers.Admin.Topics;

public class AdminTopicsController : AdminBaseController
{
    public ITopicService _topicService;
    public AdminTopicsController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] TopicCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicService.CreateAsync(dto)
        });


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] TopicUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicService.UpdateAsync(id, dto)
        });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicService.DeleteAsync(id)
        });


    [HttpDelete("destroy/{id:long}")]
    public async Task<IActionResult> DestroyAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _topicService.DestroyAsync(id)
        });
}
