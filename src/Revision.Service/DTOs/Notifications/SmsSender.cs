namespace Revision.Service.DTOs.Notifications;

public class SmsSender
{
    public string Recipient { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
}
