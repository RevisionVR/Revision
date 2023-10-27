using Revision.Domain.Commons;
using Revision.Domain.Entities.Chats.Rooms;

namespace Revision.Domain.Entities.Chats.Messages;

public class Message : Auditable
{
    public string Text { get; set; } = string.Empty;

    public long RoomId { get; set; }
    public Room Room { get; set; }
}