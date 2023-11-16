using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Users;
using Revision.Service.Validations.Users;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Users;

public class AdminUsersController : AdminBaseController
{
    private readonly IUserService _userService;
    public AdminUsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromForm] UserUpdateDto dto)
    {
        var validation = new UserUpdateDtoValidator();
        var result = await validation.ValidateAsync(dto);

        if (result.IsValid)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.UpdateAsync(id, dto)
            });
        }

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
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
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams pagination, 
        [FromQuery] string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.GetAllAsync(pagination, search)
        });
}