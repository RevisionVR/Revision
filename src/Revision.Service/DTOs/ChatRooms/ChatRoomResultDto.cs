using Revision.Service.DTOs.Educations;
using Revision.Service.DTOs.Users;

namespace Revision.Service.DTOs.ChatRooms;

public class ChatRoomResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public UserResultDto User { get; set; }
    public EducationResultDto Education { get; set; }
}
