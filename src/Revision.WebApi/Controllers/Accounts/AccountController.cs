using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;
using Revision.Service.Interfaces.Users;
using Revision.Service.Validations.Users;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Accounts;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private IUserService _userService;
    private IIdentityService _identityService;

    public AccountController(
        IIdentityService identityService,
        IUserService userService)
    {
        this._userService = userService;
        this._identityService = identityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAsync()
        => Ok(await _userService.GetByIdAsync(_identityService.Id));

    [HttpPut("information")]
    public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto dto)
    {
        var validation = new UserUpdateDtoValidator();
        var isValid = await validation.ValidateAsync(dto);

        if (isValid.IsValid)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.UpdateAsync(_identityService.Id, dto)
            });
        }

        return BadRequest(isValid.Errors);
    }

    [HttpPut("security")]
    public async Task<IActionResult> UpdateSecurityAsync([FromForm] UserSecurityUpdateDto dto)
    {
        var validation = new UserSecurityUpdateDtoValidation();
        var isValid = await validation.ValidateAsync(dto);
        if (isValid.IsValid)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.UpdateSecurityAsync(_identityService.Id, dto)
            });
        }
        return BadRequest(isValid.Errors);
    }
}
