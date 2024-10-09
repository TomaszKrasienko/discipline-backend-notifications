using discipline.core.Communication.Notifications.SignalR.Registry;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace discipline.core.Communication.Notifications.SignalR;

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

    public async Task PublishForUser(NotificationType type, Guid userId, string message)
    {
        logger.LogInformation("Sending notification with type: {0} for user: {1}", type, userId);
        var route = registry.Get(type);
        await notificationsContext.Clients.User(userId.ToString()).SendAsync(route.Method, message);
    }
}