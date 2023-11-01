using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.Interfaces.Educations;
using Revision.Service.DTOs.EducationCategories;

namespace Revision.WebApi.Controllers.Admin.Educations;

public class AdminEducationCategoriesController : AdminBaseController
{
    private readonly IEducationCategoryService _educationCategoryService;
    public AdminEducationCategoriesController(IEducationCategoryService educationCategoryService)
    {
        _educationCategoryService = educationCategoryService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] EducationCategoryCreationDto dto)
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await _educationCategoryService.CreateAsync(dto)
    });


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] EducationCategoryUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationCategoryService.UpdateAsync(id, dto)
        });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationCategoryService.DeleteAsync(id)
        });


    [HttpDelete("destroy/{id:long}")]
    public async Task<IActionResult> DestroyAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationCategoryService.DestroyAsync(id)
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GeAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationCategoryService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationCategoryService.GetAllAsync(pagination)
        });
}