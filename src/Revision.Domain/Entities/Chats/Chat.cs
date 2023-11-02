using Revision.Domain.Commons;
using Revision.Domain.Entities.Assets;

namespace Revision.Domain.Entities.Chats;

public class Chat : Auditable
{
    public string Context { get; set; } = string.Empty;

    public long ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }

    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
}