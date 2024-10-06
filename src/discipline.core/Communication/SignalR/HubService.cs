using discipline.core.Communication.SignalR.Registry;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace discipline.core.Communication.SignalR;

internal sealed class HubService(
    ILogger<HubService> logger,
    IHubRegistry registry,
    IHubContext<NotificationHub> notificationsContext) : IHubService
{
    public async Task PublishForAll(NotificationType type, string message)
    {
        logger.LogInformation("Sending notification with type: {0} for all", type);
        var route = registry.Get(type);
        await notificationsContext.Clients.All.SendAsync(route.Method, message);
    }
}