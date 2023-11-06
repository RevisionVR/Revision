using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.ResetVerification;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Auth;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IAuthService _authServise;
    public AuthController(IAuthService authService)
    {
        _authServise = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromForm] UserLoginDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _authServise.LoginAsync(dto)
        });


    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromQuery] UserResetPasswordDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _authServise.ResetPasswordAsync(dto)
        });


    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyResetPasswordAsync([FromQuery] ResetPassword dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _authServise.VerifyResetPasswordAsync(dto.Phone, dto.Code)
        });
}
