using Revision.Domain.Commons;
using Revision.Service.DTOs.Assets;
using Revision.Service.DTOs.ChatRooms;
using Revision.Service.DTOs.Users;

namespace Revision.Service.DTOs.Chats;

public class ChatResultDto : Auditable
{
    public string Context { get; set; }
    public UserResultDto User { get; set; }
    public AssetResultDto Asset { get; set; } = default;
    public ChatRoomResultDto ChatRoom { get; set; }
}