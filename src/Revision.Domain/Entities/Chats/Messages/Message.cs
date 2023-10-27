using Revision.Domain.Commons;
using Revision.Domain.Entities.Chats.Conversations;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Chats.Messages;

public class Message : Auditable
{
    public string MessageText { get; set; } = string.Empty;

    public long ConversationID { get; set; }
    public Conversation Conversation { get; set; }

    public long SenderID { get; set; }
    public Education Education { get; set; }

}
