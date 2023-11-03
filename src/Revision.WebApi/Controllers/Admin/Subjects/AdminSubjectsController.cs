using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Subjects;
using Revision.Service.Interfaces.Subjects;
using Revision.Service.Validations.Subjects;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Subjects;

public class AdminSubjectsController : AdminBaseController
{
    private readonly ISubjectService _subjectService;
    public AdminSubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] SubjectCreationDto dto)
    {
        var validation = new SubjectCreationDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _subjectService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] SubjectUpdateDto dto)
    {
        var validation = new SubjectUpdateDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _subjectService.UpdateAsync(id, dto)
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
            Data = await _subjectService.DeleteAsync(id)
        });


    [HttpDelete("destroy/{id:long}")]
    public async Task<IActionResult> DestroyAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _subjectService.DestroyAsync(id)
        });
}