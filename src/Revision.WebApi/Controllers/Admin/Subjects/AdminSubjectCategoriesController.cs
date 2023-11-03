using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.SubjectCategories;
using Revision.Service.Interfaces.Subjects;
using Revision.Service.Validations.Subjects.Categories;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Subjects;

public class AdminSubjectCategoriesController : AdminBaseController
{
    private readonly ISubjectCategoryService _subjectCategoryService;
    public AdminSubjectCategoriesController(ISubjectCategoryService subjectCategoryService)
    {
        _subjectCategoryService = subjectCategoryService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] SubjectCategoryCreationDto dto)
    {
        var validation = new SubjectCategoryCreationDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _subjectCategoryService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] SubjectCategoryUpdateDto dto)
    {
        var validation = new SubjectCategoryUpdateDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _subjectCategoryService.UpdateAsync(id, dto)
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
            Data = await _subjectCategoryService.DeleteAsync(id)
        });


    [HttpDelete("destroy/{id:long}")]
    public async Task<IActionResult> DestroyAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectCategoryService.DestroyAsync(id)
        });
}
