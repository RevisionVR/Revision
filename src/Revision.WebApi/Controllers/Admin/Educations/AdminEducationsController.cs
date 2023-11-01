using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.Educations;
using Revision.Service.Interfaces.Educations;

namespace Revision.WebApi.Controllers.Admin.Educations;

public class AdminEducationsController : AdminBaseController
{
    private readonly IEducationService _educationService;
    public AdminEducationsController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(EducationCreationDto dto)
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await _educationService.CreateAsync(dto)
    });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationService.DeleteAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationService.GetAllAsync(pagination)
        });
}