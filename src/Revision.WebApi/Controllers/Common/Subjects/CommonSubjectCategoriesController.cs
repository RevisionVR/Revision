using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Subjects;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Subjects;

public class CommonSubjectCategoriesController : BaseController
{
    private readonly ISubjectCategoryService _subjectCategoryService;
    public CommonSubjectCategoriesController(ISubjectCategoryService subjectCategoryService)
    {
        _subjectCategoryService = subjectCategoryService;
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectCategoryService.GetByIdAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectCategoryService.GetAllAsync()
        });
}
