using discipline.core.Communication.SignalR.Types;

namespace discipline.core.Communication.SignalR.Registry;

internal interface IHubRegistry
{
    void Add(BroadcastingRoute broadcastingRoute);
    BroadcastingRoute Get(NotificationType type);
    bool IsKeyExists(NotificationType type);
}