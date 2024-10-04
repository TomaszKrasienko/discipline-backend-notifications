using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.Logging;

namespace discipline.core.Communication.SignalR;

internal sealed class NotificationChat
    (ILogger<NotificationChat> logger): Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync(message);
    }
    
    public override Task OnConnected()
    {
        logger.LogInformation($"User connected");
        return base.OnConnected();
    }

    public override Task OnDisconnected(bool stopCalled)
    {
        logger.LogInformation($"User disconnected");
        return base.OnDisconnected(stopCalled);
    }
}