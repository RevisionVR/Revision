using Revision.Service.DTOs.ChatRooms;

namespace Revision.Service.DTOs.Chats;

public class ChatResultDto
{
    public long Id { get; set; }
    public string Message { get; set; }
    public ChatRoomResultDto ChatRoom { get; set; }
}