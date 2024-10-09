using discipline.core.Communication.Notifications.SignalR.Types;

namespace discipline.core.Communication.Notifications.SignalR.Registry;

internal interface IHubRegistry
{
    void Add(BroadcastingRoute broadcastingRoute);
    BroadcastingRoute Get(NotificationType type);
    bool IsKeyExists(NotificationType type);
}