using Revision.Domain.Commons;
using Revision.Domain.Entities.Users;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Chats.Rooms;

namespace Revision.Domain.Entities.Chats.Conversations;

public class Conversation : Auditable
{
    public long AdminId { get; set; }
    public User User { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }

    public long RoomId { get; set; }
    public Room Room { get; set; }
}