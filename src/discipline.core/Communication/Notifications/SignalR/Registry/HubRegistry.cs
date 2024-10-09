using discipline.core.Communication.Notifications.SignalR.Types;

namespace discipline.core.Communication.Notifications.SignalR.Registry;

internal sealed class HubRegistry : IHubRegistry
{
    private readonly Dictionary<NotificationType, BroadcastingRoute> _routes = new ();

    public void Add(BroadcastingRoute broadcastingRoute)
        => _routes.TryAdd(broadcastingRoute.Type, broadcastingRoute);

    public BroadcastingRoute Get(NotificationType type)
        => _routes.GetValueOrDefault(type);

    public bool IsKeyExists(NotificationType type)
        => _routes.Keys.Any(x => x == type);
}