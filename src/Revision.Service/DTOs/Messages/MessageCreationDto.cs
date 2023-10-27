namespace Revision.Service.DTOs.Messages;

public class MessageCreationDto
{
    public string Text { get; set; } = string.Empty;
    public long RoomId { get; set; }
}