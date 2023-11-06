using Revision.Domain.Commons;
using Revision.Service.DTOs.Chats;
using Revision.Service.DTOs.Educations;
using Revision.Service.DTOs.Users;

namespace Revision.Service.DTOs.ChatRooms;

public class ChatRoomResultDto : Auditable
{
    public string Name { get; set; }
    public UserResultDto User { get; set; }
    public EducationResultDto Education { get; set; }
    public IEnumerable<ChatResultDto> Chats { get; set; }
}