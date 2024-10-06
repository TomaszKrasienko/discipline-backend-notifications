using discipline.core.Communication.SignalR;
using discipline.core.Communication.SignalR.Registry;

namespace discipline.core.Communication;

internal sealed class BroadcastingSystemNotificationWrapper(
    IHubRegistry registry,
    IHubService hubService) : INotificationWrapper
{
    public Task Send(NotificationType type)
    {
        throw new NotImplementedException();
    }

    public bool CanByApplied(NotificationType type)
        => type == NotificationType.System && registry.IsKeyExists(type);
}