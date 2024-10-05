using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace discipline.core.Communication.SignalR;

public sealed class NotificationHub
    (ILogger<NotificationHub> logger): Hub
{
    public override Task OnConnectedAsync()
    {
        logger.LogInformation($"User connected");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        logger.LogInformation($"User disconnected");
        return base.OnDisconnectedAsync(exception);
    }
}