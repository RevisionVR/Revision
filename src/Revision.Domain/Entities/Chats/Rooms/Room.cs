using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Chats.Rooms;

public class Room : Auditable
{
    public string Name { get; set; }
}