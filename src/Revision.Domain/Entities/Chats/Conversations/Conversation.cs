using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Users;

namespace Revision.Domain.Entities.Chats.Conversations;

public class Conversation : Auditable
{
    public long AdminID { get; set; }
    public User User { get; set; }

    public long EducationId { get; set; }
    public Education Education { get; set; }
}
