using Revision.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;

namespace Revision.WebApi.Controllers.Admin.Auth;

public class AdminAuthController : AdminBaseController
{
    private IAuthService _authServise;

    public AdminAuthController(IAuthService authService)
    {
        this._authServise = authService;
    }

    [HttpPost("auth")]
    public async Task<IActionResult> CreateAsync([FromForm] UserCreationDto dto)
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await _authServise.RegisterAsync(dto)
    });
}