using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Topics;
using Revision.Service.Interfaces.Topics;
using Revision.Service.Validations.Topics;
using Revision.WebApi.Models;

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
    {
        var validation = new TopicCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _topicService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] TopicUpdateDto dto)
    {
        var validation = new TopicUpdateDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _topicService.UpdateAsync(id, dto)
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