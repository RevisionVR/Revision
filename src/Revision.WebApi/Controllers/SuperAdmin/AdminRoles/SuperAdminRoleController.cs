using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Enums;
using Revision.Service.Interfaces.Users;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.SuperAdmin.AdminRoles;

[Route("api/[controller]")]
[ApiController]
public class SuperAdminRoleController : SuperAdminBaseController
{
    private IUserService _servise;

    public SuperAdminRoleController(IUserService service)
    {
        this._servise = service;
    }

    [HttpGet("get-by/role")]
    public async Task<IActionResult> GetAllAsync([FromForm] Role role)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _servise.GetByRoleAsync(role)
        });
    }

    [HttpGet("get-by/{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        return Ok();
    }

    [HttpPut("role/{Id}")]
    public async Task<IActionResult> UpdateAsync(long Id)
    {
        return Ok();
    }
}
