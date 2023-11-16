using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.Interfaces.Subjects;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Subjects;

public class CommonSubjectController : BaseController
{
    private readonly ISubjectService _subjectService;
    public CommonSubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectService.GetByIdAsync(id)
        });


    [HttpGet("get-by-category/{subjectCategoryId:long}")]
    public async Task<IActionResult> GetBySubjectCategoryIdAsync(long subjectCategoryId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectService.GetBySubjectCategoryIdAsync(subjectCategoryId)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectService.GetAllAsync()
        });


    [HttpGet("get-all-by-pagination")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams pagination,
        [FromQuery] string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectService.GetAllAsync(pagination, search)
        });
}
