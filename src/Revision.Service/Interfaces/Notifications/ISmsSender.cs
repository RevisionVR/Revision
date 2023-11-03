using Revision.Service.DTOs.Notifications;

namespace Revision.Service.Interfaces.Notifications;

public interface ISmsSender
{
    Task<bool> SendAsync(SmsSenderDto message);
}
