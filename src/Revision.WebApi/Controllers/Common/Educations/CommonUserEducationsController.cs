using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Educations;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Educations;

public class CommonUserEducationsController : BaseController
{
    private readonly IUserEducationService _userEducationService;
    public CommonUserEducationsController(IUserEducationService userEducationService)
    {
        _userEducationService = userEducationService;
    }

    [HttpGet("get-by-education/{educationId:long}")]
    public async Task<IActionResult> GetByEducationIdAsync(long educationId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userEducationService.GetByEducationIdAsync(educationId)
        });
}