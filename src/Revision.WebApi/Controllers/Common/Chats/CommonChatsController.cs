using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Chats;
using Revision.Service.Interfaces.Chats;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Chats;

public class CommonChatsController : BaseController
{
    private readonly IChatService _chatService;
    public CommonChatsController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] ChatCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatService.CreateAsync(dto)
        });


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] ChatUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatService.UpdateAsync(id, dto)
        });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatService.DeleteAsync(id)
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatService.GetByIdAsync(id)
        });


    [HttpGet("get-by-room/{roomId:long}")]
    public async Task<IActionResult> GetByRoomIdAsync(long roomId)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await _chatService.GetByRoomIdAsync(roomId)
       });


    [HttpGet("get-by-user/{userId:long}")]
    public async Task<IActionResult> GetByUserIdAsync(long userId)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await _chatService.GetByUserIdAsync(userId)
       });
}