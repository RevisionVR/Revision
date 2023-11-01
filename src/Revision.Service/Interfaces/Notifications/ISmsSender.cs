using Revision.Service.DTOs.Notifications;

namespace Revision.Service.Interfaces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsSenderDto message);
}
