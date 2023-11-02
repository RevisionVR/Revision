using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Chats;

public class Chat : Auditable
{
    public string Context { get; set; }
    public long ChatRoomId { get; set; }
    public ChatRoom ChatRoom { get; set; }
}