using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Auth;

public class AdminAuthController : AdminBaseController
{
    private IAuthService _authServise;

    public AdminAuthController(IAuthService authService)
    {
        _authServise = authService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] UserCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _authServise.RegisterAsync(dto)
        });
}