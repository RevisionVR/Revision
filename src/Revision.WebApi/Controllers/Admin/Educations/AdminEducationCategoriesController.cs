using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.EducationCategories;
using Revision.Service.Interfaces.Educations;
using Revision.Service.Validations.Educations.Categories;
using Revision.WebApi.Models;

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
    {
        var validation = new EducationCategoryCreationDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _educationCategoryService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] EducationCategoryUpdateDto dto)
    {
        var validation = new EducationCategoryUpdateDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _educationCategoryService.UpdateAsync(id, dto)
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