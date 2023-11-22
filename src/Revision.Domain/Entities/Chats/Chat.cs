using Revision.Domain.Commons;
using Revision.Domain.Entities.Assets;
using Revision.Domain.Entities.Users;

namespace Revision.Domain.Entities.Chats;

public class Chat : Auditable
{
    public string Context { get; set; } = string.Empty;

    public long UserId { get; set; }
    public User User { get; set; }

    public long ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }

    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
}