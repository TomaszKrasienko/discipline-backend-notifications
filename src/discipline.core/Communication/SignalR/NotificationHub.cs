using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace discipline.core.Communication.SignalR;

public sealed class NotificationHub
    (ILogger<NotificationHub> logger): Hub
{
    public override Task OnConnectedAsync()
    {
        var user = Context?.User?.Identity?.Name;
        logger.LogInformation($"User {user} connected");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        logger.LogInformation($"User disconnected");
        return base.OnDisconnectedAsync(exception);
    }
}