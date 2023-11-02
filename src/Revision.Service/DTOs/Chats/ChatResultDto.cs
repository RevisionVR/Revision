using Revision.Service.DTOs.Assets;
using Revision.Service.DTOs.ChatRooms;

namespace Revision.Service.DTOs.Chats;

public class ChatResultDto
{
    public long Id { get; set; }
    public string Message { get; set; }
    public AssetResultDto Asset { get; set; } = default;
    public ChatRoomResultDto ChatRoom { get; set; }
}