using discipline.core.Communication.SignalR.Registry;

namespace discipline.core.Communication;

internal sealed class BroadcastingNotificationWrapper(
    IRoutesRegistry routesRegistry) : INotificationWrapper
{
    public Task Send(NotificationType type)
    {
        throw new NotImplementedException();
    }

    public bool CanByApplied(NotificationType type)
        => routesRegistry.IsKeyExists(type);
}