using Revision.Domain.Commons;
using Revision.Domain.Entities.Users;

namespace Revision.Domain.Entities.Chats.Rooms;

public class AdminRoom : Auditable
{
    public long RoomId { get; set; }
    public Room Room { get; set; }

    public long AdminId { get; set; }
    public User User { get; set; }
}