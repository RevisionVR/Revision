using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Educations;
using Revision.Service.Interfaces.Educations;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Educations;

public class CommonEducationsController : BaseController
{
    private readonly IEducationService _educationService;
    public CommonEducationsController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] EducationUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationService.UpdateAsync(id, dto)
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GeAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationService.GetByIdAsync(id)
        });
}