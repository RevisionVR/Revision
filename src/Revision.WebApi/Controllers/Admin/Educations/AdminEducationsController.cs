using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.Educations;
using Revision.Service.Interfaces.Educations;
using Revision.Service.Validations.Educations;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Educations;

public class AdminEducationsController : AdminBaseController
{
    private readonly IEducationService _educationService;
    public AdminEducationsController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] EducationCreationDto dto)
    {
        var validation = new EducationCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _educationService.CreateAsync(dto)
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