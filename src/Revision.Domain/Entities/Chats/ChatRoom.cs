﻿using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Users;

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