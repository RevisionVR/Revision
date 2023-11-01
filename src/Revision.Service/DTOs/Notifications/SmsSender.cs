namespace Revision.Service.DTOs.Notifications;

public class SmsSenderDto
{
    public string Recipient { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
