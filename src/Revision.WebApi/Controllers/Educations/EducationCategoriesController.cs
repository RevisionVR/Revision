﻿using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.EducationCategories;
using Revision.Service.Interfaces.Educations;
using Revision.WebApi.Controllers.Common;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Educations;

public class EducationCategoriesController : BaseController
{
    private readonly IEducationCategoryService _educationCategoryService;
    public EducationCategoriesController(IEducationCategoryService educationCategoryService)
    {
        _educationCategoryService = educationCategoryService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(EducationCategoryCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _educationCategoryService.CreateAsync(dto)
        });


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, EducationCategoryUpdateDto dto)
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


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
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