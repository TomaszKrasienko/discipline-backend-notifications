using discipline.core.Communication.Notifications.SignalR;
using discipline.core.Communication.Notifications.SignalR.Registry;
using discipline.core.Serializer;

namespace discipline.core.Communication.Notifications;

internal sealed class BroadcastingSystemNotificationWrapper(
    IHubRegistry registry,
    IHubService hubService,
    ISerializer serializer) : INotificationWrapper
{
    private const NotificationType Type = NotificationType.System;
    
    public async Task SendForAll(object message)
    {
        var serializedMessage = serializer.ToJson(message);
        await hubService.PublishForAll(Type, serializedMessage);
    }

    public async Task SendForUser(Guid userId, object message)
    {
        var serializedMessage = serializer.ToJson(message);
        await hubService.PublishForUser(Type, userId, serializedMessage);
    }

    public bool CanByApplied(NotificationType type)
        => type == Type && registry.IsKeyExists(type);
}