using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.ChatRooms;
using Revision.Service.Interfaces.Chats;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Common.Chats;

public class CommonChatRoomsController : BaseController
{
    private readonly IChatRoomService _chatRoomService;
    public CommonChatRoomsController(IChatRoomService chatRoomService)
    {
        _chatRoomService = chatRoomService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] ChatRoomCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatRoomService.CreateAsync(dto)
        });


    [HttpDelete("destroy/{id:long}")]
    public async Task<IActionResult> DestroyAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatRoomService.DestroyAsync(id)
        });


    [HttpGet("get-by-user/{userId:long}")]
    public async Task<IActionResult> GetByRoomIdAsync(long userId)
       => Ok(new Response
       {
           StatusCode = 200,
           Message = "Success",
           Data = await _chatRoomService.GetByUserIdAsync(userId)
       });


    [HttpGet("get-by-education/{educationId:long}")]
    public async Task<IActionResult> GetAsync(long educationId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatRoomService.GetByEducationIdAsync(educationId)
        });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _chatRoomService.GetAllAsync()
        });
}