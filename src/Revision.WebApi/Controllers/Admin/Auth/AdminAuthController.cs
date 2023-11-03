using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Logins;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;
using Revision.Service.Validations.Users;

namespace Revision.WebApi.Controllers.Admin.Auth;

[ApiController]
[Route("api/[controller]")]
public class AdminAuthController : AdminBaseController
{
    private IAuthService _authServise;
    public AdminAuthController(IAuthService authService)
    {
        _authServise = authService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] UserCreationDto dto)
    {
        var validator = new UserCreationDtoValidator();
        var validation = await validator.ValidateAsync(dto);

        if (validation.IsValid)
            return Ok(await _authServise.RegisterAsync(dto));

        return BadRequest(validation.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromForm] UserLoginDto dto)
    {
        var result = await _authServise.LoginAsync(dto);

        return Ok(result);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromQuery] UserResetPasswordDto dto)
    {
        var result = await _authServise.ResetPasswordAsync(dto);

        return Ok(result);
    }

    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyResetPasswordAsync([FromQuery] ResetPassword dto)
    {
        var result = await _authServise.VerifyResetPasswordAsync(dto.Phone, dto.Code);

        return Ok(result);
    }
}
