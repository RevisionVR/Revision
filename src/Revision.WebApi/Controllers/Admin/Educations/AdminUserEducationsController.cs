using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.UserEducations;
using Revision.Service.Interfaces.Educations;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Educations;

public class AdminUserEducationsController : AdminBaseController
{
    private readonly IUserEducationService _userEducationService;
    public AdminUserEducationsController(IUserEducationService userEducationService)
    {
        _userEducationService = userEducationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(UserEducationCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userEducationService.CreateAsync(dto)
        });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userEducationService.DeleteAsync(id)
        });

    [HttpGet("get-by-user/{userId:long}")]
    public async Task<IActionResult> GetByUserIdAsync(long userId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userEducationService.GetByUserIdAsync(userId)
        });
}