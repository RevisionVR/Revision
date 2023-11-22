using Microsoft.AspNetCore.Http;

namespace Revision.Service.DTOs.Chats;

public class ChatUpdateDto
{
    public string Context { get; set; }
    public long UserId { get; set; }
    public long ChatRoomId { get; set; }
    public IFormFile FormFile { get; set; } = null;
}
