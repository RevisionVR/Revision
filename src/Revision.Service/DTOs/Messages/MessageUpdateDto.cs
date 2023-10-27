namespace Revision.Service.DTOs.Messages;

public class MessageUpdateDto
{
    public string Text { get; set; } = string.Empty;
    public long RoomId { get; set; }
}