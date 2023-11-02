using Revision.Domain.Commons;
using Revision.Domain.Entities.Users;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Chats;

public class ChatRoom : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }
}