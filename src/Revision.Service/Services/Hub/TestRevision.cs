using Microsoft.AspNetCore.SignalR;

namespace Revision.Service.Services.Hubs;

public class TestRevision : Hub
{
    private List<string> _revisionsDevice;


    public async Task Auth(string? deviceId, bool isTeacher, string? Subject)
    {
        if (!IsDeviceExist(deviceId))
        {
            string DeviceGuidId = Guid.NewGuid().ToString();
            _revisionsDevice.Add(DeviceGuidId);

            await Clients.Caller.SendAsync("ReceiveMessage", DeviceGuidId);
        }
        else
        {
            if (isTeacher)
            {
                await Console.Out.WriteLineAsync(Subject);
                await Clients.Others.SendAsync("ReceiveMessage", Subject);
            }
        }
    }

    private bool IsDeviceExist(string deviceId)
    {
        foreach (var revision in _revisionsDevice)
        {
            Console.WriteLine(revision);
        }

        if (_revisionsDevice == null)
            return false;
        else if (_revisionsDevice.Contains(deviceId))
            return true;
        else
            return false;
    }

    private static List<string> ids = new List<string>();
    private static readonly List<string> ConnectedClients = new List<string>();


    public async Task BroadcastMessage(float x, float y, float z)
    {

        if (!ids.Contains(Context.ConnectionId.ToString()))
        {
            Console.WriteLine(Context.ConnectionId);
            ids.Add(Context.ConnectionId.ToString());

            Console.WriteLine(ids[ids.Count - 1]);
        }

        Console.WriteLine(ids.IndexOf(Context.ConnectionId.ToString()));

        int id = ids.IndexOf(Context.ConnectionId.ToString());
        await Clients.Others.SendAsync("OnMessageReceived", id, x, y, z);
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override Task OnConnectedAsync()
    {
        ConnectedClients.Add(Context.ConnectionId.ToString());
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        ConnectedClients.Remove(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}