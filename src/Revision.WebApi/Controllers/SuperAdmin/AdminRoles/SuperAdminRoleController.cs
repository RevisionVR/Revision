using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Enums;
using Revision.Service.Interfaces.Users;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.SuperAdmin.AdminRoles;

public class SuperAdminRoleController : SuperAdminBaseController
{
    private IUserService _userService;
    public SuperAdminRoleController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get-by/{role}")]
    public async Task<IActionResult> GetAllAsync([FromForm] Role role)
         => Ok(new Response
         {
             StatusCode = 200,
             Message = "Success",
             Data = await _userService.GetByRoleAsync(role)
         });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.GetByIdAsync(id)
        });


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, Role role)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.UpgradeRoleAsync(id, role)
        });
}