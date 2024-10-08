using discipline.core.Communication.SignalR;
using discipline.core.Communication.SignalR.Registry;
using discipline.core.Serializer;

namespace discipline.core.Communication;

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

    public Task SendForUser(Guid userId, object message)
    {
        throw new NotImplementedException();
    }

    public bool CanByApplied(NotificationType type)
        => type == Type && registry.IsKeyExists(type);
}