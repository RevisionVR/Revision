using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;
using Revision.Service.Interfaces.Users;
using Revision.Service.Validations.Users;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Accounts;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private IUserService _userService;
    private IIdentityService _identityService;
    public AccountController(
        IIdentityService identityService,
        IUserService userService)
    {
        _userService = userService;
        _identityService = identityService;
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto dto)
    {
        var validation = new UserUpdateDtoValidator();
        var result = await validation.ValidateAsync(dto);

        if (result.IsValid)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.UpdateAsync(_identityService.Id, dto)
            });
        }

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPut("change/password")]
    public async Task<IActionResult> UpdateSecurityAsync([FromForm] UserSecurityUpdateDto dto)
    {
        var validation = new UserSecurityUpdateDtoValidation();
        var result = await validation.ValidateAsync(dto);
        if (result.IsValid)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.UpdateSecurityAsync(_identityService.Id, dto)
            });
        }

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpGet("get")]
    public async Task<IActionResult> GetUserAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.GetByIdAsync(_identityService.Id)
        });
}