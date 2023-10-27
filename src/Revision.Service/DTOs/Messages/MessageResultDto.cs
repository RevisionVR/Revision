using Revision.Service.DTOs.Rooms;

namespace Revision.Service.DTOs.Messages;

public class MessageResultDto
{
    public long Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public RoomResultDto Room { get; set; }
}
