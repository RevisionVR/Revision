using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.ResetVerification;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;
using Revision.Service.Validations.Users;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Auth;

public class AdminAuthController : AdminBaseController
{
    private IAuthService _authServise;
    public AdminAuthController(IAuthService authService)
    {
        _authServise = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateAsync([FromForm] UserCreationDto dto)
    {
        var validation = new UserCreationDtoValidator();
        var result = await validation.ValidateAsync(dto);

        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _authServise.RegisterAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
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