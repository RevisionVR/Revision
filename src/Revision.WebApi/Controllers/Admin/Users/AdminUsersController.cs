﻿using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.Interfaces.Users;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Users;

public class AdminUsersController : AdminBaseController
{
    private readonly IUserService _userService;
    public AdminUsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.GetByIdAsync(id)
        });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.DeleteAsync(id)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.GetAllAsync(pagination)
        });
}