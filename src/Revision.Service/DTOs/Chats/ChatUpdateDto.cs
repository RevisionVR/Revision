using Microsoft.AspNetCore.Http;

namespace Revision.Service.DTOs.Chats;

public class ChatUpdateDto
{
    public string Message { get; set; }
    public long ChatRoomId { get; set; }
    public IFormFile FormFile { get; set; } = null;
}
