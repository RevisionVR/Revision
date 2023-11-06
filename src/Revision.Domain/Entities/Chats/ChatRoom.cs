using Revision.Domain.Commons;
using Revision.Domain.Entities.Users;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Chats;

public class ChatRoom : Auditable
{
    public string Name { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }

    public ICollection<Chat> Chats { get; set; }
}